using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics;


namespace EchoEditor
{
    public partial class Form1 : Form
    {
        //global variable
        string tid = "";
        string technique = "";
        string tactic = "";
        Dictionary<string, List<string>> mitreDict = new Dictionary<string, List<string>>();
        string curdir = Directory.GetCurrentDirectory();
        string OutPutFolder = "";
        string TemplateFolder = "";
        string SigmaNameFormat = "";
        string Team = "";
        string MitreDomain = "";
        string SigmacFolder = "";
        string Author = "";
        string Id = "";
        string PythonPath = "";
        int OldCursorIndex = 0;
        int oldLineLength = 0;
        //string selected_Technique = "";
        RichTextBox activeLineNum = new RichTextBox();
        RichTextBox activeRichTextBox = new RichTextBox();
        Dictionary<int, int> dictTermPtrs = new Dictionary<int, int>();

        string tokens = "(id:|title:|status:|version:|description:|author:|reference:|references:|logsource:|category:" +
                 "|product:|service:|detection:|selection:|condition:|fields:|falsepositives:" +
                 "|severity:|generateticket:|lookup:|tags:)";
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*
             * this is is the initialization for loading the form 
             */
            Inittemplate();
            SiemComboBox.Sorted = true;
            SiemComboBox.SelectedIndex = 5;
            LoadMitreAttackdata(CreateXmlFile());
            createNewPage();
            GetSettingInfo();
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// MenuBar
        /////////////////////////////////////////////////////////////////////////////////

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] EventClick: newToolStripButton_Click");
            //create new tab page 
            createNewPage();

            //refresh the tabcontrol page 
            tabControl1.Refresh();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] EventClick: openToolStripButton_Click");
            //function helper for opening file
            OpenFileHelper();

            //load template color
            LoadTermsFontColor();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] EventClick: cutToolStripButton_Click");
            FindActiveWorkSpace();
            activeRichTextBox.Cut();

        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] EventClick: copyToolStripButton_Click");
            FindActiveWorkSpace();
            activeRichTextBox.Copy();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] EventClick: pasteToolStripButton_Click");
            FindActiveWorkSpace();
            activeRichTextBox.Paste();
            //LoadTermsFontColor();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] EventClick: newToolStripMenuItem_Click");
            createNewPage();
            tabControl1.Refresh();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] EventClick: closeToolStripMenuItem_Click");
            for (int i = 0; i < tabControl1.Controls.Count; i++)
            {
                if (tabControl1.SelectedTab == tabControl1.TabPages[i])
                {
                    if ((tabControl1.Controls[i].Text).Contains("TabPage"))
                    {
                        bool flag = AskMessageBox("Are you sure you want to close this TabPage?", "Unsaved File");
                        if (flag == false)
                        {
                            continue;
                        }
                    }
                    System.Diagnostics.Debug.WriteLine(tabControl1.Controls[i].Text);
                    System.Diagnostics.Debug.WriteLine("active: " + tabControl1.TabPages[i]);
                    tabControl1.TabPages.Remove(tabControl1.TabPages[i]);
                    break;
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] EventClick: openToolStripMenuItem_Click");
            OpenFileHelper();
            LoadTermsFontColor();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] EventClick: saveToolStripButton_Click");
            SaveFileMenu();

        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] EventClick: cutToolStripMenuItem_Click");
            FindActiveWorkSpace();
            activeRichTextBox.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] EventClick: copyToolStripMenuItem_Click");
            FindActiveWorkSpace();
            activeRichTextBox.Copy();

        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] EventClick: pasteToolStripMenuItem_Click");
            FindActiveWorkSpace();
            activeRichTextBox.Paste();
            //LoadTermsFontColor();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] EventClick: undoToolStripMenuItem_Click");
            FindActiveWorkSpace();
            activeRichTextBox.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] EventClick: redoToolStripMenuItem_Click");
            FindActiveWorkSpace();
            activeRichTextBox.Redo();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] EventClick: saveToolStripMenuItem_Click");
            SaveFileMenu();
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// utilities shortcut button
        /////////////////////////////////////////////////////////////////////////////////

        private void SaveFileMenu()
        {
            int TabIndex = FindActiveTabPage();
            if (tabControl1.TabPages[TabIndex].Text.Contains("TabPage"))
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                string t_id = tid.Replace("technique", "").Replace("T", "").Split('/').Last();
                saveFileDialog1.FileName = SigmaNameFormat.Replace("<tid>", t_id).Replace("<technique>", technique);
                System.Diagnostics.Debug.WriteLine(string.Format("SaveFileName: {0}", saveFileDialog1.FileName));
                SaveFileDialog_Attrib(saveFileDialog1);
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //change the name of the tabpage
                    tabControl1.SelectedTab.Text = saveFileDialog1.FileName;

                    //save the file
                    SaveFileHelper(TabIndex);
                }

            }
            else
            {
                SaveFileHelper(TabIndex);
            }
        }

        //exit menuitem
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] EventClick: exitToolStripMenuItem_Click");
            this.Close();
        }

        private void loadInitButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] EventClick: loadInitButton_Click");
            ReplaceStrInActiveRichtextBox();
            LoadTermsFontColor();

        }

        private void mitreButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] EventClick: mitreButton_Click");
            downloadMitreWebPage();
        }
        
        // template button
        private void loadTempButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] EventClick: loadTempButton_Click");
            // get the selected combobox item through its index as pointer
            string selected_temp = templateComboBox.Items[templateComboBox.SelectedIndex].ToString();
            string tempFullPath = Path.Combine(curdir, "templates", selected_temp);
            if (File.Exists(tempFullPath))
            {
                using (StreamReader stp = File.OpenText(tempFullPath))
                {
                    string buffer = stp.ReadToEnd();
                    FindActiveWorkSpace();
                    activeRichTextBox.Text = buffer;
                    activeRichTextBox.Refresh();
                    activeRichTextBox.Focus();
                    ShowLineNum();
                    LoadTermsFontColor();
                }
                
            }


        }

        //close button
        private void closeButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] EventClick: closeButton_Click");
            this.Close();
        }

        //setting button
        private void settingButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] EventClick: settingButton_Click");
            settingsForm st = new settingsForm();

            //populate the setting text box
            st.outTextBox.Text = GetSettingInfo().Item1;
            st.templTextBox.Text = GetSettingInfo().Item2;
            st.sigmaNameTextBox.Text = GetSettingInfo().Item3;
            st.teamTextBox.Text = GetSettingInfo().Item4;
            st.mdomainTextBox.Text = GetSettingInfo().Item5;
            st.sigmacTextBox.Text = GetSettingInfo().Item6;
            st.authorTextBox.Text = GetSettingInfo().Item7;
            st.IDtextBox.Text = GetSettingInfo().Rest.Item1;
            st.pythonPathTextBox.Text = GetSettingInfo().Rest.Item2;

            //show it
            st.ShowDialog();
            
        }

        //search button
        private void searchButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] EventClick: searchButton_Click");
            string terms = SearchTextBox.Text;
            SearchTerms(terms, true);

        }

   
        //// helper functions
        ////////////////////////////////////////////////////////////////////////////////
        ////

        private void Inittemplate()
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : Inittemplate");
            string template_path = Path.Combine(curdir, "templates");
            if (Directory.Exists(template_path))
            {
                foreach (string template_file in Directory.GetFiles(template_path))
                {
                    templateComboBox.Items.Add(Path.GetFileName(template_file));
                }
                // sort the value
                templateComboBox.Sorted = true;

                //set initial value
                templateComboBox.SelectedIndex = 0;
            }
            else
            {
                WarningMessageBox((String.Format("Folder Path: {0} is not exist", template_path)), "Warning: Folder not Exist");
            }


        }

        private int FindActiveTabPage()
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : FindActiveTabPage");
            int i = 0;
            for (i = 0; i < tabControl1.Controls.Count; i++)
            {
                if (tabControl1.SelectedTab == tabControl1.TabPages[i])
                {
                    break;
                }
            }
            return i;
        }

        private void SaveFileHelper(int TabIndex)
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : SaveFileHelper");
            //look for the active richtextbox for work place area
            string FilePath = tabControl1.TabPages[TabIndex].Text;
            foreach (RichTextBox activeRTB in tabControl1.SelectedTab.Controls)
            {
                if (activeRTB.Name.Contains("rtbWorkPlace"))
                {
                    //save the richtextbox.text to a file
                    activeRTB.SaveFile(FilePath, RichTextBoxStreamType.PlainText);

                    //update the chosenSigmaFiletextBox with the current file path of the save file
                    chosenSigmaFiletextBox.Text = FilePath;
                }
            }

        }

        private bool AskMessageBox(string message, string title)
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : AskMessageBox");
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void WarningMessageBox(string message, string title)
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : WarningMessageBox");
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, title, buttons);
        }

        private void SaveFileDialog_Attrib(SaveFileDialog saveFileDialog)
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : SaveFileDialog_Attrib");
            saveFileDialog.InitialDirectory = curdir;
            saveFileDialog.Title = "Save Files";
            saveFileDialog.CheckFileExists = false;
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.DefaultExt = "*.sigma";
            saveFileDialog.Filter = "Yml File(*.yml)|*yml|sigma Files (*.sigma)|*.sigma|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;
        }

        private void createNewPage()
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : createNewPage");
            // create new tab page
            string title = "TabPage " + (tabControl1.TabCount).ToString();
            TabPage myTabPage = new TabPage(title);
            tabControl1.TabPages.Add(myTabPage);
            tabControl1.SelectedTab = myTabPage;

            //create new richtextbox inside the tab page with fill style
            RichTextBox rtbWorkPlace = new RichTextBox();
            myTabPage.Controls.Add(rtbWorkPlace);

            newRichTextBoxControls(rtbWorkPlace);

            //new properties of the line number
            RichTextBox rtbLineNum = new RichTextBox();
            myTabPage.Controls.Add(rtbLineNum);

            newLineNumRTB(rtbLineNum);

            // refresh the tabcontrol
            tabControl1.Refresh();
            
        }

        private void newRichTextBoxControls(RichTextBox rtbWorkPlace)
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : newRichTextBoxControls");
            // new properties of the working place
            rtbWorkPlace.AcceptsTab = true;
            rtbWorkPlace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
| System.Windows.Forms.AnchorStyles.Left)
| System.Windows.Forms.AnchorStyles.Right)));
            rtbWorkPlace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            rtbWorkPlace.EnableAutoDragDrop = true;
            rtbWorkPlace.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //rtbWorkPlace.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            rtbWorkPlace.ForeColor = Color.Bisque;
            rtbWorkPlace.Location = new System.Drawing.Point(44, 6);
            rtbWorkPlace.Name = "rtbWorkPlace" + (tabControl1.TabCount).ToString();
            //int winHeight = Screen.PrimaryScreen.Bounds.Height;
            //int winWidth = Screen.PrimaryScreen.Bounds.Width;
            //rtbWorkPlace.Dock = DockStyle.Fill;
            rtbWorkPlace.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            rtbWorkPlace.Size = new System.Drawing.Size(1838, 630);
            rtbWorkPlace.TabIndex = 0;
            rtbWorkPlace.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox_TextChanged);
            //rtbWorkPlace.TextChanged += new System.EventHandler(this.richTextBox_TextChanged2);
            

        }

        private void newLineNumRTB(RichTextBox rtbLineNum)
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : newLineNumRTB");
            //rtbLineNum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)System.Windows.Forms.AnchorStyles.Left)));
            rtbLineNum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top)
| System.Windows.Forms.AnchorStyles.Left)));
            rtbLineNum.BackColor = System.Drawing.SystemColors.Info;
            rtbLineNum.BorderStyle = System.Windows.Forms.BorderStyle.None;
            rtbLineNum.Font = new System.Drawing.Font("Consolas", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Regular | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            rtbLineNum.ForeColor = System.Drawing.Color.Green;
            rtbLineNum.Location = new System.Drawing.Point(0, 6);
            rtbLineNum.Name = "rtbLineNum" + (tabControl1.TabCount).ToString();
            rtbLineNum.ReadOnly = true;
            //rtbLineNum.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            rtbLineNum.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            rtbLineNum.Size = new System.Drawing.Size(38, 629);
            rtbLineNum.TabIndex = 10;
            rtbLineNum.Text = "";
            rtbLineNum.Cursor = System.Windows.Forms.Cursors.PanNE;
            rtbLineNum.HideSelection = false;
            
            //rtbLineNum.VScroll += new System.Windows.Forms.VScrollBar(this.rtbLineNum_VScroll;

        }

        private void OpenFileHelper()
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : OpenFileHelper");
            try
            {
                // open a dialog box to find some files to open
                OpenFileDialog openFile = new OpenFileDialog();
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    FindActiveWorkSpace();
                    //change the tab file name
                    tabControl1.SelectedTab.Text = openFile.FileName;

                    //change the chosen textbox 
                    chosenSigmaFiletextBox.Text = openFile.FileName;

                    Console.WriteLine(tabControl1.SelectedTab.Name);
                    //read the file and place it to the working place
                    using (StreamReader sr = new StreamReader(openFile.FileName))
                    {
                        activeRichTextBox.Text = sr.ReadToEnd();
                        sr.Close();
                    }
                    //refresh the form
                    tabControl1.Refresh();
                    chosenSigmaFiletextBox.Refresh();
                }
            }
            catch (Exception)
            {
                WarningMessageBox("WARNING: Work Space is not exists,\nPlease Create \"New\" to resolve the problem", "WorkSpace Not Exist");
            }
        }

        private void downloadMitreWebPage()
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : downloadMitreWebPage");
            bool flag = AskMessageBox("Are you sure to Download Mitre data in [https://attack.mitre.org]", "Download Mitre Data");
            if (flag)
            {
                string urlLink = "https://attack.mitre.org";
                string localFile = "mitre_data.html";
                string mitreFolderName = "mitre_data";
                string mitreDataDirPath = Path.Combine(curdir, mitreFolderName);
                if (!Directory.Exists(mitreDataDirPath))
                {
                    WarningMessageBox((String.Format("Folder Path: {0} is not exist", mitreDataDirPath)), "Warning: Folder not Exist");
                }
                else
                {
                    string localHtmlPath = Path.Combine(mitreDataDirPath, localFile);
                    downloadWebPagehelper(urlLink, localHtmlPath);
                    HtmlParserHelper(localHtmlPath);

                }
            }
        }

        private void downloadWebPagehelper(string urlLink, string localHtmlPath)
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : downloadWebPagehelper");
            WebClient client = new WebClient();
            client.DownloadFile(urlLink, localHtmlPath);
            HtmlParserHelper(localHtmlPath);
        }

        private void HtmlParserHelper(string localHtmlPath)
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : HtmlParserHelper");
            List<string> hrefTags = new List<string>();
            HtmlAgilityPack.HtmlDocument hap = new HtmlAgilityPack.HtmlDocument();
            hap.Load(localHtmlPath);
            System.Diagnostics.Debug.WriteLine(hap);

            // parsing html
            HtmlNode[] nodes = hap.DocumentNode.SelectNodes("//a[@class=\"technique-mapping\"]").ToArray();

            //looking for each href nodes
            string tid="";
            string mitreXmlFile = "";
            string xmlFile = CreateXmlFile();
            foreach (HtmlNode item in nodes)
            {
                
                string ptn = @"href=(.*)>.*</a>";
                Regex rgx = new Regex(ptn);
                
                foreach (Match match in rgx.Matches(item.OuterHtml))
                {
                    tid = (match.Groups[1].Value).Replace("\"","");   
                }

                //remove extra character
                string mtTactic= item.Id.Replace(item.InnerHtml, "").Replace("v--","").Replace("-tab","").Replace(" ", "-");
                string mtTechnique = (item.InnerHtml).Replace(" ", "-").Replace("/","-");
                mitreXmlFile = XmlWriterHelper(xmlFile, mtTactic, mtTechnique, tid);
            }
            LoadMitreAttackdata(mitreXmlFile);
        }

        private string CreateXmlFile()
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : CreateXmlFile");
            string mdFolder = "mitre_data";
            string mdPath = Path.Combine(curdir, mdFolder);
            string mdXmlFile = Path.Combine(mdPath, "mitre_data_info.xml");
            if (!File.Exists(mdXmlFile))
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlNode rootNode = xmlDoc.CreateElement("mitreAttack");
                xmlDoc.AppendChild(rootNode);
                xmlDoc.Save(mdXmlFile);

            }
            return mdXmlFile;
        }

        private string XmlWriterHelper(string xmlFile, string mtTactic, string mtTechnique, string tid)
        {
               System.Diagnostics.Debug.WriteLine("[+] Function Trigger : XmlWriterHelper");

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFile);
                XmlNode rootNode = xmlDoc.SelectSingleNode("mitreAttack");
                XmlNode mtTechq = xmlDoc.CreateElement("MitreAttackComponent");

                //create attribute for tid and technique values
                XmlAttribute tidAttrib = xmlDoc.CreateAttribute("tid");
                XmlAttribute techAttrib = xmlDoc.CreateAttribute("technique");
                tidAttrib.Value = tid;
                techAttrib.Value = mtTechnique;

                //append the values
                mtTechq.Attributes.Append(tidAttrib);
                mtTechq.Attributes.Append(techAttrib);
                mtTechq.InnerText = mtTactic;
                rootNode.AppendChild(mtTechq);

                xmlDoc.Save(xmlFile);


            return xmlFile;

        }

        private void LoadMitreAttackdata(string mitreXmlFile)
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : LoadMitreAttackdata");
            //parse the xml for mitre table
            XmlDocument xmlMitre = new XmlDocument();
            xmlMitre.Load(mitreXmlFile);
            XmlNodeList elemList = xmlMitre.GetElementsByTagName("MitreAttackComponent");
            for (int i=0; i< elemList.Count;i++)
            {
                tid = elemList[i].Attributes["tid"].Value;
                technique = elemList[i].Attributes["technique"].Value;
                tactic = elemList[i].InnerXml;
                //Console.WriteLine(String.Format("{0} {1} {2}", technique, tid, tactic));
                
                createDictList(mitreDict, technique, tactic, tid);
            }

            foreach (KeyValuePair<string, List<string>> kvp in mitreDict)
            {

                toolTechniqueComboBox.Items.Add(Path.GetFileName(kvp.Key));
                toolTechniqueComboBox.Sorted = true;
                //set initial value
                // create dictionary out of xml table.
                //Console.WriteLine("Key = {0}", kvp.Key);
                //for (int i = 0; i < kvp.Value.Count; i++)
                //{
                //    Console.WriteLine("Value = {0}", kvp.Value[i]);
                //}
                // sort the value
            }



        }

        private void createDictList(Dictionary<string, List<string>> mitreDict, string technique, string tactic, string tid)
        {

            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : createDictList");
            //check if the key is exist alerady
            if (mitreDict.ContainsKey(technique))
            {
                //get list value
                List<string> listValue = mitreDict[technique];
                if (listValue.Contains(tid) == false)
                {
                    listValue.Add(tid);
                }
                else if (listValue.Contains(tactic) == false)
                {
                    listValue.Add(tactic);
                }
            }
            else
            {
                List<string> listValue = new List<string>();
                listValue.Add(tid);
                listValue.Add(tactic);
                mitreDict.Add(technique, listValue);
            }

        }

        private void toolTechniqueComboBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : toolTechniqueComboBox1_Click");
            tacticTextBox.Text = "";
            string selected_Technique = toolTechniqueComboBox.Items[toolTechniqueComboBox.SelectedIndex].ToString();
            System.Diagnostics.Debug.WriteLine(string.Format("[+] selected technique: {0}", selected_Technique));

            List<string> listValue = mitreDict[selected_Technique];
            tidTextBox.Text = listValue[0];
            for (int i = 1; i < listValue.Count; i++)
            {
                tacticTextBox.AppendText(listValue[i] + ";");
            }

        }

        private void chooseSigmaFileButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : chooseSigmaFileButton_Click");
            
            OpenFileDialog openSigmaFile = new OpenFileDialog
            {
                InitialDirectory = curdir,
                Title = "Browse Sigma Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "*.sigma",
                Filter = "sigma Files (*.sigma)|*.sigma|Yml File(*.yml)|*yml|Text files (*.txt)|*.txt|All files (*.*)|*.*",
                RestoreDirectory = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true,
            };
            if (openSigmaFile.ShowDialog() == DialogResult.OK)
            {
                chosenSigmaFiletextBox.Text = openSigmaFile.FileName;
            }

        }

        private void richTextBox_TextChanged(object sender, KeyEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : richTextBox_TextChanged");
            
            ShowLineNum();
            activeRichTextBox.SelectionColor = Color.Bisque;
            


            if (e.KeyCode == Keys.Escape)
            {
                RemoveSearchHighlight(dictTermPtrs);
            }

            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            //if (e.KeyCode == Keys.Space )
            {
                TermsFontColor();
            }
        }

        private void ShowLineNum()
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : ShowLineNum");
            
            FindActiveWorkSpace();
            //get the current line number
            int index = activeRichTextBox.SelectionStart;
            int lineNumber = activeRichTextBox.GetLineFromCharIndex(index);

            //get the index of the first character in the current line position
            int f = activeRichTextBox.GetFirstCharIndexFromLine(lineNumber);
            int col = (index - f);

            //update the line and column info label below the current working area
            System.Diagnostics.Debug.WriteLine(string.Format("Line:{0} Column:{1} f:{2}", lineNumber + 1, col, f));
            string info = string.Format("Line: {0} | Columns: {1}", lineNumber + 1, col);
            lineColInfotextBox1.Text = info;
            int lineNum = lineNumber + 1;
            int lineCount = activeRichTextBox.Lines.Count();


            //update linenumber richtextbox
            int charCount = 0;
            int row = 0;
            if (oldLineLength != lineCount)
            {
                activeLineNum.Clear();
                for (int x = 1; x <= lineNum; x++)
                {
                    activeLineNum.Text += x.ToString() + "\n";
                }
                //automatic scroll
                activeLineNum.SelectionStart = activeLineNum.TextLength;
                activeLineNum.ScrollToCaret();
                oldLineLength = lineNum;
            }

            //
            else if (lineCount > lineNum)
            {
                
                for (int i = 0; i < lineNum; i++)
                {

                    charCount += activeLineNum.Lines[i].Length;
                    row++;
                }
                Console.WriteLine(string.Format("{0},{1},{2} {3}", oldLineLength, lineCount, lineNum, row));
                Console.WriteLine(charCount);
                activeLineNum.SelectionStart = charCount-1;
                if (lineNum !=row)
                {
                    activeLineNum.ScrollToCaret();
                }
            }
            else
            {
                
                Console.WriteLine(string.Format("else: {0},{1},{2}", oldLineLength, lineCount, lineNum));
            }
            //oldLineLength = lineNum;
            //else if (oldLineLength > lineNum)
            //{
            //    int charCount = 0;
            //    int row = 0;
            //    for (int i = 0; i < lineNum; i++)
            //    {

            //        charCount += activeLineNum.Lines[i].Length;
            //        Console.WriteLine(charCount);
            //        row++;
            //        activeLineNum.SelectionStart = charCount;
            //        activeLineNum.ScrollToCaret();
            //    }

            //    int charCount2 = (activeLineNum.TextLength - activeLineNum.Lines[lineNumber].Length) - 1;
            //}




        }

        public Tuple<string, string, string, string, string, string, string, Tuple<string,string>> GetSettingInfo()
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : GetSettingInfo");
            
            string settingXml = Path.Combine(curdir, "setting.xml");
            XmlDocument xmlSetting = new XmlDocument();
            xmlSetting.Load(settingXml);

            // place the value to a variable
            OutPutFolder = xmlSetting.GetElementsByTagName("OutPutFolder").Item(0).InnerText;
            TemplateFolder = xmlSetting.GetElementsByTagName("TemplateFolder").Item(0).InnerText;
            SigmaNameFormat = xmlSetting.GetElementsByTagName("SigmaNameFormat").Item(0).InnerText;
            Team = xmlSetting.GetElementsByTagName("Team").Item(0).InnerText;
            MitreDomain = xmlSetting.GetElementsByTagName("MitreDomain").Item(0).InnerText;
            SigmacFolder = xmlSetting.GetElementsByTagName("SigmacFolder").Item(0).InnerText;
            Author = xmlSetting.GetElementsByTagName("Author").Item(0).InnerText;
            Id = xmlSetting.GetElementsByTagName("Id").Item(0).InnerText;
            PythonPath = xmlSetting.GetElementsByTagName("PythonPath").Item(0).InnerText;
            var settingValues = new Tuple<string, string, string, string, string, string, string, Tuple<string,string>> (OutPutFolder, TemplateFolder, SigmaNameFormat, Team, MitreDomain, SigmacFolder, Author, new Tuple<string,string>(Id,PythonPath));
            return settingValues;
        }

        private void ReplaceStrInActiveRichtextBox()
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : ReplaceStrInActiveRichtextBox");
            
            ////parse tid
            string [] tidVal = (tidTextBox.Text).Replace("techniques","").Split('/');
            

            ////parse tactic
            string[] tacticVal = (tacticTextBox.Text).Split(';');

            ////combine tactic and tid array and remove empyty values; var x is each values in the array
            string[] TidTacticVal = (tacticVal.Concat(tidVal).ToArray()).Where(x => !string.IsNullOrEmpty(x)).ToArray();

            ////new ID            
            //int lastIndex = tidVal.Length - 1;
            string newId = string.Format("id: {0}",Id.Replace("<tid>", tidVal.Last().Replace("T","")).Replace("<technique>", toolTechniqueComboBox.Items[toolTechniqueComboBox.SelectedIndex].ToString()));
            LookForSubStrLine("id:", newId);

            ////new title
            string newTitle = string.Format("title: {0}", toolTechniqueComboBox.Items[toolTechniqueComboBox.SelectedIndex].ToString());
            LookForSubStrLine("title:",newTitle);

            ////new author
            string newAuthor = string.Format("author: {0},{1}", Author, Team);
            LookForSubStrLine("author:", newAuthor);

            ////new reference
            string newRef = string.Format("reference: {0}{1}", MitreDomain, tidTextBox.Text);
            LookForSubStrLine("reference:", newRef);
            LookForSubStrLine("references:", newRef);

            ////add tags
            LookForTags("tags", TidTacticVal);


        }

        private void LookForSubStrLine(string FieldValues, string NewFieldValue)
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : LookForSubStrLine");
            

            foreach (string line in activeRichTextBox.Lines)
            {
                if (line.Contains(FieldValues))
                {
                    activeRichTextBox.Text = activeRichTextBox.Text.Replace(line, NewFieldValue);
                    break;
                }
            }
            //TermsFontColor();
            
            //activeRichTextBox.Refresh();

        }

        private void LookForTags(string FieldValues, string[] TidTacticVal)
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : LookForTags");

            int LinesCount = activeRichTextBox.Lines.Count() - 1;
            int _line = 0;
            int tacticCountChecker = 0;
            int TidTacticCount = TidTacticVal.Count() - 1;
            for (_line = 0; _line < LinesCount; _line++)
            {

                if (activeRichTextBox.Lines[_line].Contains(FieldValues))
                {
                    //remove the appended attack tags
                    for (int i = _line + 1; i < LinesCount; i++)
                    {
                        // Find the first char position of that line inside the text buffer
                        int start_index = activeRichTextBox.GetFirstCharIndexFromLine(i);
                        int a = activeRichTextBox.Lines[i].Length;
                        if (activeRichTextBox.Lines[i].Contains("attack") || activeRichTextBox.Lines[i].Length == 0)
                        {
                            //removed the whole line
                            activeRichTextBox.Text = activeRichTextBox.Text.Remove(start_index, activeRichTextBox.Lines[i].Length);
                            activeRichTextBox.Refresh();
                            if (tacticCountChecker <= TidTacticCount)
                            {
                                string newTags = string.Format("  - attack.{0}", TidTacticVal[tacticCountChecker].ToLower().Replace("-","_"));
                                activeRichTextBox.Text = activeRichTextBox.Text.Insert(start_index, newTags);
                                tacticCountChecker++;
                            }

                        }  
                    }
                }
                
                
            }
        }

        private void FindActiveWorkSpace()
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : FindActiveWorkSpace");
            //RichTextBox activeRichTextBox = new RichTextBox();
            try
            {
                foreach (RichTextBox activeRtb in tabControl1.SelectedTab.Controls)
                {
                    if (activeRtb.Name.Contains("rtbLineNum"))
                    {
                        activeLineNum = activeRtb;
                    }
                    if (activeRtb.Name.Contains("rtbWorkPlace"))
                    {
                        activeRichTextBox = activeRtb;
                    }
                }
            }
            catch (Exception)
            {
                WarningMessageBox("Next Time don't close the Root Tab :).", "Closing Root Tab");
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : tabControl1_SelectedIndexChanged");
            FindActiveWorkSpace();
        }

        private void LoadTermsFontColor()
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : LoadTermsFontColor");
            Regex rex = new Regex(tokens);
            var lines = activeRichTextBox.Text.Split('\n');
            int termIndex = 0;
            int termLen = 0;
            for(int k = 0; k< lines.Count();k++)
            {
                //MatchCollection mc = rex.Matches(activeRichTextBox.Text);
                MatchCollection mc = rex.Matches(lines[k]);
                int StartCursorIndex = activeRichTextBox.SelectionStart;
                int linestartIndex = activeRichTextBox.GetFirstCharIndexFromLine(k);
                if (mc.Count > 0)
                {
                    foreach (Match m in mc)
                    {
                        termIndex = m.Index + linestartIndex;
                        termLen = m.Length;
                        activeRichTextBox.Select(termIndex, termLen);
                        activeRichTextBox.SelectionColor = Color.LightSeaGreen;
                        activeRichTextBox.Select(termIndex + termLen, lines[k].Length);
                        activeRichTextBox.SelectionColor = Color.Bisque;
                    }

                }
                else
                {
                    activeRichTextBox.Select(linestartIndex, lines[k].Length);
                    activeRichTextBox.SelectionColor = Color.Bisque;
                    activeRichTextBox.SelectionStart = activeRichTextBox.Text.Length;
                    //activeRichTextBox.DeselectAll();
                }


            }

        }

        private void SearchTerms(string term, bool SearchFlag)
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : SearchTerms");
            string tokens = term;
            Regex rex = new Regex(tokens);
            MatchCollection mc = rex.Matches(activeRichTextBox.Text);
            int StartCursorIndex = activeRichTextBox.SelectionStart;
            dictTermPtrs.Clear();
            foreach (Match m in mc)
            {
                int termIndex = m.Index;
                int termEndIndex = m.Length;
                if (!dictTermPtrs.ContainsKey(termIndex))
                {
                    dictTermPtrs.Add(termIndex, termEndIndex);
                }
                activeRichTextBox.Select(termIndex, termEndIndex);
                activeRichTextBox.SelectionBackColor = Color.LightSkyBlue;
                activeRichTextBox.DeselectAll();
            }

        }

        private void RemoveSearchHighlight(Dictionary<int,int> dictTermPtrs)
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : RemoveSearchHighlight");
            foreach (KeyValuePair<int,int> entry in dictTermPtrs)
            {
                activeRichTextBox.Select(entry.Key, entry.Value);
                activeRichTextBox.SelectionBackColor = activeRichTextBox.BackColor;
                activeRichTextBox.DeselectAll();
            }
        }

        private void TermsFontColor()
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : TermsFontColor");
            Regex rex = new Regex(tokens);
            int StartCursorIndex = activeRichTextBox.SelectionStart;
            int termSize = StartCursorIndex - OldCursorIndex;
            activeRichTextBox.Select(OldCursorIndex, termSize);
            string terms = activeRichTextBox.SelectedText;
            MatchCollection mc = rex.Matches(terms);

            int termIndex = 0;
            int termLen = 0;
            if (mc.Count > 0)
            {
                foreach(Match m in mc)
                {
                    Console.WriteLine(m.Index);
                    //termIndex = StartCursorIndex-m.Length;
                    termIndex = OldCursorIndex + m.Index;
                    termLen = m.Length;
                    activeRichTextBox.Select(termIndex, termLen);
                    activeRichTextBox.SelectionColor = Color.LightSeaGreen;

                }
            }
            //activeRichTextBox.SelectionStart = StartCursorIndex;
            activeRichTextBox.Select(StartCursorIndex, 0);
            activeRichTextBox.SelectionColor = Color.Bisque;
            activeRichTextBox.Focus();
            OldCursorIndex = StartCursorIndex;

        }

        private void sigmaConverterButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : sigmaConverterButton_Click");
            string sigmaFile = chosenSigmaFiletextBox.Text;
            string selectedSiemFormat = SiemComboBox.Items[SiemComboBox.SelectedIndex].ToString();

            string sigmaRulePath = chosenSigmaFiletextBox.Text;
            string sigmacFilePath = Path.Combine(curdir, SigmacFolder, "sigmac");
            string args = string.Format("{0} -t {1} {2} -D -v", sigmacFilePath, selectedSiemFormat, sigmaRulePath); 
            string result = RunCmd(PythonPath, args);
            SigmacRichTextBox.Clear();
            SigmacRichTextBox.Text = result;
            SigmacRichTextBox.Refresh();
        }

        private string RunCmd(string cmd, string args)
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : RunCmd");
            string result = "";
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = cmd;
            startInfo.Arguments = args;
            startInfo.UseShellExecute = false;// Do not use OS shell
            startInfo.CreateNoWindow = true; // We don't need new window
            startInfo.RedirectStandardOutput = true;// Any output, generated by application will be redirected back
            startInfo.RedirectStandardError = true; // Any error in standard output will be redirected back (for example exceptions)
            using (Process process = Process.Start(startInfo))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string stderr = process.StandardError.ReadToEnd(); // Here are the exceptions from our Python script
                    result = reader.ReadToEnd(); // Here is the result of StdOut(for example: print "test")
                    
                }
            }
            return result;
                
        }

        private void CopySigmacButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : CopySigmacButton_Click");
            SigmacRichTextBox.SelectAll();
            SigmacRichTextBox.Copy();
            string SiemFmt = SiemComboBox.Items[SiemComboBox.SelectedIndex].ToString();
            SiemFmt = String.Format("{0}_.log", SiemFmt);
            string sigmaLogs = Path.ChangeExtension(chosenSigmaFiletextBox.Text, SiemFmt);
            
            try
            {
                if (File.Exists(sigmaLogs))
                {
                    File.Delete(sigmaLogs);
                }
                using (StreamWriter sw = File.CreateText(sigmaLogs))
                {
                    sw.WriteLine(SigmacRichTextBox.Text);
                }
            }
            catch (Exception ex)
            {
                WarningMessageBox(ex.ToString(), "sigma conversion logs error");
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[+] Function Trigger : aboutToolStripMenuItem_Click");
            AboutEchoEditor echo = new AboutEchoEditor();
            echo.ShowDialog();
        }


        //private void richTextBox_TextChanged(object sender, EventArgs e)
        //{
        //    ShowLineNum();
        //}


        /*
        private void TabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("mouse click");
            int i = 0;
            // check if the right mouse button was pressed            
            if (e.Button == MouseButtons.Right)
            {
                // iterate through all the tab pages
                for (i = 0; i < tabControl1.TabCount; i++)
                {
                    Rectangle r = tabControl1.GetTabRect(i);
                    if (r.Contains(e.Location))
                    {
                        System.Diagnostics.Debug.WriteLine("Tabpressed: " + i);
                    }
                }
            }

        }

        private void TabControl1_MouseUp(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("mouse release");
        }
        */


    }
}
