/*
# Copyright 2019 tccontre

# This file is part of EchoEditor.

# EchoEditor is free software: you can redistribute it and/or modify
# it under the terms of the GNU General Public License as published by
# the Free Software Foundation, either version 3 of the License, or
# (at your option) any later version.

# EchoEditor is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU General Public License for more details.

# You should have received a copy of the GNU General Public License
# along with EchoEditor.  If not, see <http://www.gnu.org/licenses/>.

*/

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

namespace EchoEditor
{
    public partial class settingsForm : Form
    {
        public settingsForm()
        {
            InitializeComponent();
        }

        private void settingsForm_Load(object sender, EventArgs e)
        {

        }

        public string outTextBoxValue
        {
            get
            {
                return outTextBox.Text;
            }

        }

        private void editbutton2_Click(object sender, EventArgs e)
        {
            readOnlytextBox(false);
        }

        private void readOnlytextBox(bool flag)
        {
            templTextBox.ReadOnly = flag;
            sigmaNameTextBox.ReadOnly = flag;
            teamTextBox.ReadOnly = flag;
            mdomainTextBox.ReadOnly = flag;
            sigmacTextBox.ReadOnly = flag;
            authorTextBox.ReadOnly = flag;
            outTextBox.ReadOnly = flag;
            IDtextBox.ReadOnly = flag;
            pythonPathTextBox.ReadOnly = flag;
        }
        private void savebutton1_Click(object sender, EventArgs e)
        {
            readOnlytextBox(true);
            string settingXml = Path.Combine(Directory.GetCurrentDirectory(), "setting.xml");
            XmlDocument xmlSetting = new XmlDocument();
            xmlSetting.Load(settingXml);

            XmlNode OutPutFolder = xmlSetting.DocumentElement["OutPutFolder"];
            OutPutFolder.FirstChild.InnerText = outTextBox.Text;
            XmlNode TemplateFolder = xmlSetting.DocumentElement["TemplateFolder"];
            TemplateFolder.FirstChild.InnerText = templTextBox.Text;
            XmlNode SigmaNameFormat = xmlSetting.DocumentElement["SigmaNameFormat"];
            SigmaNameFormat.FirstChild.InnerText = sigmaNameTextBox.Text;
            XmlNode Team = xmlSetting.DocumentElement["Team"];
            Team.FirstChild.InnerText = teamTextBox.Text;
            XmlNode MitreDomain = xmlSetting.DocumentElement["MitreDomain"];
            MitreDomain.FirstChild.InnerText = mdomainTextBox.Text;
            XmlNode SigmacFolder = xmlSetting.DocumentElement["SigmacFolder"];
            SigmacFolder.FirstChild.InnerText = sigmacTextBox.Text;
            XmlNode Author = xmlSetting.DocumentElement["Author"];
            Author.FirstChild.InnerText = authorTextBox.Text;
            XmlNode Id = xmlSetting.DocumentElement["Id"];
            Id.FirstChild.InnerText = IDtextBox.Text;
            XmlNode PythonPath = xmlSetting.DocumentElement["PythonPath"];
            PythonPath.FirstChild.InnerText = pythonPathTextBox.Text;
            xmlSetting.Save(settingXml);
            
           
        }

        //private void TriggerGetSettingInfo()
        //{
        //    Form1 form1 = new Form1();
        //    form1.GetSettingInfo();
        //    form1.Refresh();
        //}

        //private void TriggerGetSettingInfo(object sender, FormClosedEventArgs e)
        //{
        //    TriggerGetSettingInfo();
        //}
    }
}
