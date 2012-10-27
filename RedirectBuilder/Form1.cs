using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace RedirectBuilder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpenFileDialog_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "MS Project Files (*.mpp)|*.mpp|" + "All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
                txtFileName.Text = dlg.FileName;
        }

        private void btnCreateRedirect_Click(object sender, EventArgs e)
        {
            String strFileNameAll = System.IO.Path.GetFileName(txtFileName.Text);
            String strFileNameOnly = System.IO.Path.GetFileNameWithoutExtension(txtFileName.Text);
            String strNewFile = strFileNameAll + ".html";

            // File fNewHtml = new File(strFileNameAll + ".html");

            FileStream outputfile = null;

            try
            {
                outputfile = new FileStream(strNewFile, FileMode.OpenOrCreate, FileAccess.Write);


                StreamWriter writer = new StreamWriter(outputfile);

                writer.BaseStream.Seek(0, SeekOrigin.End);

                writer.WriteLine("<Script type=\"text/javascript\">");
                writer.Flush();
                writer.WriteLine("var projectFile = \"V:\\\\FmsCore\\\\ProjMgmt\\\\01_ProgMgmt\\\\01_ScheduleMgmt\\\\SubProjects\\\\" + strFileNameAll + "\";");
                writer.Flush();
                writer.WriteLine("window.location = \"file://\" + projectFile;");
                writer.Flush();
                writer.WriteLine("</Script><html xmlns:mso=\"urn:schemas-microsoft-com:office:office\" xmlns:msdt=\"uuid:C2F41010-65B3-11d1-A29F-00AA00C14882\"><head>");
                writer.Flush();
                writer.WriteLine("<!--[if gte mso 9]><xml>");
                writer.Flush();
                writer.WriteLine("<mso:CustomDocumentProperties>");
                writer.Flush();
                writer.WriteLine("<mso:Doc_x0020_Cat msdt:dt=\"string\">Project Management</mso:Doc_x0020_Cat>");
                writer.Flush();
                writer.WriteLine("<mso:Deliverable msdt:dt=\"string\">0</mso:Deliverable>");
                writer.Flush();
                writer.WriteLine("<mso:Tags msdt:dt=\"string\"></mso:Tags>");
                writer.Flush();
                writer.WriteLine("<mso:ContentType msdt:dt=\"string\">Document</mso:ContentType>");
                writer.Flush();
                writer.WriteLine("<mso:Doc_x0020_Type msdt:dt=\"string\">Project Schedule</mso:Doc_x0020_Type>");
                writer.Flush();
                writer.WriteLine("<mso:Document_x0020_Type msdt:dt=\"string\">;#In Progress;#</mso:Document_x0020_Type>");
                writer.Flush();
                writer.WriteLine("<mso:_Comments msdt:dt=\"string\"></mso:_Comments>");
                writer.Flush();
                writer.WriteLine("</mso:CustomDocumentProperties>");
                writer.Flush();
                writer.WriteLine("</xml><![endif]-->");
                writer.Flush();
                writer.WriteLine("<title>" + strFileNameAll + "</title></head>");
                writer.Flush();

                writer.Close();

                MessageBox.Show("File Created Successfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Generate Code = " + ex);
                outputfile = null;
                // writer = null;

                MessageBox.Show("Error!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
