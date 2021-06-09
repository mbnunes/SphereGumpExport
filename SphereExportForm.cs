// Decompiled with JetBrains decompiler
// Type: SphereGumpExport.SphereExportForm
// Assembly: SphereGumpExport, Version=1.0.1840.31013, Culture=neutral, PublicKeyToken=null
// MVID: 0BC49E18-EADD-45F5-BD25-BC779861FAFE
// Assembly location: C:\GumpStudio_1_8_R3_quinted-02\Plugins\SphereGumpExport.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SphereGumpExport
{
  public class SphereExportForm : Form
  {
    private Container components = (Container) null;
    private Label lbl_Help;
    private GroupBox grp_Sphereversion;
    private RadioButton rbt_Sphere56;
    private RadioButton rbt_Sphere1;
    private GroupBox grp_Properties;
    private TextBox txt_Gumpname;
    private Label lbl_Gumpname;
    private TextBox txt_Savefile;
    private GroupBox grp_Saveas;
    private Button btn_Browse;
    private Button btn_Cancel;
    private Button btn_Export;
    private SphereExporter seWorker;

    public string GumpName
    {
      get
      {
        return this.txt_Gumpname.Text;
      }
    }

    public SphereExportForm(SphereExporter seJob)
    {
      this.InitializeComponent();
      this.seWorker = seJob;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.lbl_Help = new Label();
      this.grp_Sphereversion = new GroupBox();
      this.rbt_Sphere56 = new RadioButton();
      this.rbt_Sphere1 = new RadioButton();
      this.grp_Properties = new GroupBox();
      this.lbl_Gumpname = new Label();
      this.txt_Gumpname = new TextBox();
      this.txt_Savefile = new TextBox();
      this.grp_Saveas = new GroupBox();
      this.btn_Browse = new Button();
      this.btn_Cancel = new Button();
      this.btn_Export = new Button();
      this.grp_Sphereversion.SuspendLayout();
      this.grp_Properties.SuspendLayout();
      this.grp_Saveas.SuspendLayout();
      this.SuspendLayout();
      this.lbl_Help.BackColor = SystemColors.Info;
      this.lbl_Help.Location = new Point(0, 0);
      this.lbl_Help.Name = "lbl_Help";
      this.lbl_Help.Size = new Size(336, 32);
      this.lbl_Help.TabIndex = 0;
      this.lbl_Help.Text = "Select your Sphere version, a gump name and where to save the gump.";
      this.lbl_Help.TextAlign = ContentAlignment.MiddleCenter;
      this.grp_Sphereversion.Controls.Add((Control) this.rbt_Sphere56);
      this.grp_Sphereversion.Controls.Add((Control) this.rbt_Sphere1);
      this.grp_Sphereversion.Location = new Point(0, 40);
      this.grp_Sphereversion.Name = "grp_Sphereversion";
      this.grp_Sphereversion.Size = new Size(160, 80);
      this.grp_Sphereversion.TabIndex = 1;
      this.grp_Sphereversion.TabStop = false;
      this.grp_Sphereversion.Text = "Sphere Version";
      this.rbt_Sphere56.Checked = true;
      this.rbt_Sphere56.Location = new Point(16, 24);
      this.rbt_Sphere56.Name = "rbt_Sphere56";
      this.rbt_Sphere56.Size = new Size(128, 16);
      this.rbt_Sphere56.TabIndex = 3;
      this.rbt_Sphere56.TabStop = true;
      this.rbt_Sphere56.Text = "0.56 ( and Revision )";
      this.rbt_Sphere1.Location = new Point(16, 48);
      this.rbt_Sphere1.Name = "rbt_Sphere1";
      this.rbt_Sphere1.Size = new Size(128, 24);
      this.rbt_Sphere1.TabIndex = 3;
      this.rbt_Sphere1.Text = "0.99 and 1.0";
      this.grp_Properties.Controls.Add((Control) this.lbl_Gumpname);
      this.grp_Properties.Controls.Add((Control) this.txt_Gumpname);
      this.grp_Properties.Location = new Point(168, 40);
      this.grp_Properties.Name = "grp_Properties";
      this.grp_Properties.Size = new Size(160, 80);
      this.grp_Properties.TabIndex = 2;
      this.grp_Properties.TabStop = false;
      this.grp_Properties.Text = "Properties";
      this.lbl_Gumpname.Location = new Point(16, 24);
      this.lbl_Gumpname.Name = "lbl_Gumpname";
      this.lbl_Gumpname.Size = new Size(136, 16);
      this.lbl_Gumpname.TabIndex = 1;
      this.lbl_Gumpname.Text = "Gump name:";
      this.txt_Gumpname.Location = new Point(16, 48);
      this.txt_Gumpname.Name = "txt_Gumpname";
      this.txt_Gumpname.Size = new Size(136, 20);
      this.txt_Gumpname.TabIndex = 0;
      this.txt_Gumpname.Text = "d_default";
      this.txt_Savefile.BackColor = SystemColors.ControlLight;
      this.txt_Savefile.Enabled = false;
      this.txt_Savefile.Location = new Point(8, 24);
      this.txt_Savefile.Name = "txt_Savefile";
      this.txt_Savefile.Size = new Size(224, 20);
      this.txt_Savefile.TabIndex = 3;
      this.txt_Savefile.Text = "";
      this.grp_Saveas.Controls.Add((Control) this.btn_Browse);
      this.grp_Saveas.Controls.Add((Control) this.txt_Savefile);
      this.grp_Saveas.Location = new Point(0, 128);
      this.grp_Saveas.Name = "grp_Saveas";
      this.grp_Saveas.Size = new Size(328, 56);
      this.grp_Saveas.TabIndex = 4;
      this.grp_Saveas.TabStop = false;
      this.grp_Saveas.Text = "Save As...";
      this.btn_Browse.Location = new Point(240, 24);
      this.btn_Browse.Name = "btn_Browse";
      this.btn_Browse.Size = new Size(80, 24);
      this.btn_Browse.TabIndex = 4;
      this.btn_Browse.Text = "Browse";
      this.btn_Browse.Click += new EventHandler(this.btn_Browse_Click);
      this.btn_Cancel.Location = new Point(136, 200);
      this.btn_Cancel.Name = "btn_Cancel";
      this.btn_Cancel.Size = new Size(88, 24);
      this.btn_Cancel.TabIndex = 5;
      this.btn_Cancel.Text = "Cancel";
      this.btn_Cancel.Click += new EventHandler(this.btn_Cancel_Click);
      this.btn_Export.Location = new Point(232, 200);
      this.btn_Export.Name = "btn_Export";
      this.btn_Export.Size = new Size(88, 23);
      this.btn_Export.TabIndex = 6;
      this.btn_Export.Text = "Export";
      this.btn_Export.Click += new EventHandler(this.btn_Export_Click);
      this.AutoScaleBaseSize = new Size(5, 13);
      this.ClientSize = new Size(330, 234);
      this.ControlBox = false;
      this.Controls.Add((Control) this.btn_Export);
      this.Controls.Add((Control) this.btn_Cancel);
      this.Controls.Add((Control) this.grp_Properties);
      this.Controls.Add((Control) this.grp_Sphereversion);
      this.Controls.Add((Control) this.lbl_Help);
      this.Controls.Add((Control) this.grp_Saveas);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      ((Control) this).Location = new Point(256, 184);
      this.Name = "SphereExportForm";
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Sphere Export Options";
      this.grp_Sphereversion.ResumeLayout(false);
      this.grp_Properties.ResumeLayout(false);
      this.grp_Saveas.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    private void btn_Cancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btn_Browse_Click(object sender, EventArgs e)
    {
      SaveFileDialog saveFileDialog = new SaveFileDialog();
      saveFileDialog.Filter = "Sphere Script (*.scp)|*.scp";
      if (saveFileDialog.ShowDialog() != DialogResult.OK)
        return;
      this.txt_Savefile.Text = saveFileDialog.FileName;
    }

    private void btn_Export_Click(object sender, EventArgs e)
    {
      if (this.txt_Savefile.Text.Length == 0)
      {
        int num1 = (int) MessageBox.Show("Please Specify the File Name.");
      }
      else if (this.txt_Gumpname.Text.Length == 0)
      {
        int num2 = (int) MessageBox.Show("Please Specify the Gump Name.");
      }
      else
      {
        StreamWriter text = File.CreateText(this.txt_Savefile.Text);
        StringWriter sphereScript = this.seWorker.GetSphereScript(this.rbt_Sphere56.Checked);
        text.Write(sphereScript.ToString());
        sphereScript.Close();
        text.Close();
        this.Close();
      }
    }
  }
}
