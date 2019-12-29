using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SKSECosaveCleaner
{
    public partial class MainWindow : Form
    {
        private string m_currentFilename = "";
        private byte[] m_currentBuffer = null;
        private byte[] m_sectionEndBuffer = null;
        private int m_startIndex = -1;
        private int m_endIndex = -1;
        private int m_headerEndIndex = -1;
        private int m_sectionTotalSize = 0;

        private readonly string MAGIC_NUM = "EEKS";

        public MainWindow()
        {
            InitializeComponent();

            btnClean.Enabled = false;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            // Display Dialog
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;
            LoadSaveFile(ofd.FileName);
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            // Collect all the actor exclusions. Allows splits by comma, whitespace, newline, etc.
            string[] excludedActors = tbExcludedActors.Text.Split(new string[]
            {
                ",",
                " ",
                "\r\n",
                ""
            }, StringSplitOptions.RemoveEmptyEntries);

            ArrayList excludedActorIDs = new ArrayList(10);
            bool playerFlag = false;
            for(int i = 0; i < excludedActors.Length; i++)
            {
                try
                {
                    ActorID actor = new ActorID(excludedActors[i]);
                    excludedActorIDs.Add(actor);
                    if(BitConverter.ToInt32(actor.ID, 0) == 20)
                    {
                        playerFlag = true;
                    }
                }
                catch(ArgumentException ex)
                {

                }
            }

            // Warn the user about the player data not being excluded!
            if(!playerFlag)
            {
                DialogResult result = MessageBox.Show("The player characters form id (00000014) was not found in the exclusion list! Proceeding will result in loss of data like RaceMenu slider settings and scaling for you character.\nDo you still wish to proceed? ", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result != DialogResult.OK)
                    return;
            }

            // Pull the records of the excluded actors and store their offets for later writing.
            ArrayList actorSections = null;
            if(excludedActorIDs.Count > 0 && m_headerEndIndex != -1)
            {
                bool @ignoringLoadOrder = cbIgnoreLoadOrder.Checked;
                actorSections = new ArrayList(excludedActorIDs.Count * 3);

                for (int i = m_headerEndIndex + 15; i < m_endIndex; i++)
                {
                    int sectionStart = -1;

                    if (m_currentBuffer[i] == 255 && m_currentBuffer[i + 1] == 255 &&
                       m_currentBuffer[i + 2] == 0 && m_currentBuffer[i + 3] == 0)
                    {
                        sectionStart = i - 16;

                        // Actor ID bytes. They are reversed in the file... so read them reversed.
                        byte[] idbytes = new byte[]
                        {
                        m_currentBuffer[i - 4],
                        m_currentBuffer[i - 3],
                        m_currentBuffer[i - 2],
                        m_currentBuffer[i - 1]
                        };

                        // Compare actor bytes against the exclusion list.
                        bool actorFound = false;
                        foreach (ActorID obj in excludedActorIDs)
                        {
                            byte[] id = obj.ID;
                            int numidBytes = @ignoringLoadOrder ? 3 : 4; // Number of bytes depends if we care about the load order or not.
                            for (int j = 0; (j < numidBytes) && (id[j] == idbytes[j]); j++)
                            {
                                if (j == numidBytes - 1)
                                    actorFound = true;
                            }
                            if (actorFound)
                            {
                                obj.IDFound = true;
                                break;
                            }
                        }

                        // Find the ending offset for this actors record.
                        int sectionEnd = -1;
                        for (int j = sectionStart + 18; j < m_endIndex; j++)
                        {
                            if (m_currentBuffer[j] == 69 && m_currentBuffer[j + 1] == 69) // Section ends in EE (Indicates end of the whole thing usually. EETI).
                            {
                                sectionEnd = j - 1;
                                break;
                            }

                            if (m_currentBuffer[j] == 255 && m_currentBuffer[j + 1] == 255 &&
                               m_currentBuffer[j + 2] == 0 && m_currentBuffer[j + 3] == 0) // Normal section ending.
                            {
                                sectionEnd = j - 17;
                                break;
                            }
                        }

                        // If the actor was found we add it to our sections list.
                        if (actorFound)
                        {
                            actorSections.Add(new ActorSection(sectionStart, sectionEnd));
                        }

                        // Advance i by the total size of this section.
                        i += sectionEnd - sectionStart;
                    }
                }
            }

            // Write cleaned file to disk.
            string tempFilename = Path.GetDirectoryName(m_currentFilename) + "/skse.tmp";
            bool writeSuccess = false;

            FileStream tempFile = null;
            try
            {
                // Open the temp file for writing.
                tempFile = File.Create(tempFilename);

                // Write all the data that comes before the SKEE data.
                tempFile.Write(this.m_currentBuffer, 0, m_startIndex);

                // Write excluded actor sections.
                bool hasActorSections = false;
                if(actorSections != null && actorSections.Count > 0)
                {
                    hasActorSections = true;

                    // Write 8 bytes from the starting index.
                    tempFile.Write(m_currentBuffer, m_startIndex, 8);
                    // Write the header 8 bytes from the start index to 8 bytes from the end.
                    tempFile.Write(m_currentBuffer, m_startIndex + 8, m_headerEndIndex - m_startIndex - 8);

                    // Sort the actor sections by id.

                    // Write the actor sections to file.
                    foreach(ActorSection section in actorSections)
                    {
                        int start = section.m_startIndex;
                        int length = section.m_endIndex - start;
                        tempFile.Write(m_currentBuffer, start, length + 1);
                    }
                    tempFile.Write(m_sectionEndBuffer, 0, m_sectionEndBuffer.Length);
                }

                // Calculate delta size.
                int size = (m_endIndex - m_startIndex + 1);
                int delta = size - ((int)tempFile.Position - m_startIndex);
                // Write the rest of the file if there is anything left.
                if(m_endIndex < m_currentBuffer.Length)
                {
                    tempFile.Write(m_currentBuffer, m_endIndex, m_currentBuffer.Length - m_endIndex);
                }

                lblDeltaSize.Text = Math.Round(-(double)delta / 1024.0, 2) + " KB";

                // We had excluded actors so write the delta segment.
                if (hasActorSections)
                {
                    byte[] bytes = BitConverter.GetBytes(size - 12);
                    tempFile.Position = (long)(m_startIndex + 8);
                    tempFile.Write(bytes, 0, bytes.Length);
                }

                writeSuccess = true;
            }
            catch(Exception ex)
            {

            }
            finally
            {
                if(tempFile != null)
                {
                    tempFile.Close();
                }
            }

            if (!writeSuccess)
                return;

            // Overrwrite save with cleaned version and make a backup of previous.
            try
            {
                File.Replace(tempFilename, m_currentFilename, m_currentFilename + ".bak");
            }
            catch(Exception ex)
            {

            }

            MessageBox.Show("Cleaned Succesfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnClean.Enabled = false;
        }

        private void ResetData()
        {
            // Reset labels
            lblTotalSize.Text = "0 KB";
            lblPercentSize.Text = "0%";
            lblDeltaSize.Text = "0";

            // Section ending
            m_sectionEndBuffer = new byte[20];
            m_sectionEndBuffer[0] = 69; // E
            m_sectionEndBuffer[1] = 69; // E
            m_sectionEndBuffer[2] = 84; // T
            m_sectionEndBuffer[3] = 73; // H
            m_sectionEndBuffer[4] = 1; // SOH control character

            // Pad with 0's from 5th to 19th byte. 0index.
            for(int i = 5; i < 20; i++)
            {
                m_sectionEndBuffer[i] = 0;
            }

            // 8th and 12th byte have BS ctrl chara and SOH control chara respectively.
            m_sectionEndBuffer[8] = 8;
            m_sectionEndBuffer[12] = 1;
        }

        private void LoadSaveFile(string filename)
        {
            m_currentFilename = filename;

            try
            {
                ResetData();
                
                // Pull the save file data into byte buffer.
                m_currentBuffer = File.ReadAllBytes(filename);

                // Scan byte buffer for the magic num.
                //int i = 0;
                int len = m_currentBuffer.Length;
                for (int i = 0; i < len; i++)
                {
                    bool valid = true;
                    for (int j = 0; j < MAGIC_NUM.Length && valid; j++)
                    {
                        byte n = m_currentBuffer[i + j];
                        if (n != MAGIC_NUM[j])
                        {
                            valid = false;
                            break;
                        }
                    }

                    if (valid)
                    {
                        m_startIndex = i;
                    }
                }

                // If magic num found we continue.
                if(m_startIndex != -1)
                {
                    // Find the end of the records as indicated by the section end buffers bytes.
                    int sectionIndex = m_startIndex;
                    int sectionLen = m_currentBuffer.Length - m_sectionEndBuffer.Length + 1;
                    while(sectionIndex < sectionLen)
                    {
                        if(m_sectionEndBuffer[0] == m_currentBuffer[sectionIndex])
                        {
                            for(int i = 0; i < 4; i++)
                            {
                                if(m_sectionEndBuffer[i] != m_currentBuffer[sectionIndex + i])
                                {
                                    m_endIndex = -1;
                                    break;
                                }

                                m_endIndex = sectionIndex + m_sectionEndBuffer.Length;
                            }

                            if(m_endIndex != -1)
                            {
                                break;
                            }
                        }

                        sectionIndex++;
                    }

                    if(m_startIndex == -1 || m_endIndex == -1)
                    {
                        MessageBox.Show("Couldn't find the end section!");
                        return;
                    }

                    // Find header ending index.
                    for(int i = m_startIndex; i < m_endIndex - 4; i++)
                    {
                        if(m_currentBuffer[i] == 255 && m_currentBuffer[i + 1] == 255 &&
                           m_currentBuffer[i + 2] == 0 && m_currentBuffer[i + 3] == 0)
                        {
                            m_headerEndIndex = i - 16;
                            break;
                        }
                    }

                    m_sectionTotalSize = m_endIndex - m_startIndex + 1;
                    lblTotalSize.Text = Math.Round((double)m_sectionTotalSize / 1024.0, 2) + " KB";
                    lblPercentSize.Text = Math.Round((double)m_sectionTotalSize / (double)m_currentBuffer.Length * 100.0, 1) + "%";
                    //Console.WriteLine("Section size: " + m_sectionTotalSize);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            btnClean.Enabled = true;
        }


        private long FindStartIndex()
        {
            return -1;
        }
        private long FindEndIndex()
        {
            return -1;
        }
    }
}
