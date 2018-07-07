// Decompiled with JetBrains decompiler
// Type: SphereGumpExport.SphereExporter
// Assembly: SphereGumpExport, Version=1.0.1840.31013, Culture=neutral, PublicKeyToken=null
// MVID: 0BC49E18-EADD-45F5-BD25-BC779861FAFE
// Assembly location: C:\GumpStudio_1_8_R3_quinted-02\Plugins\SphereGumpExport.dll

using GumpStudio;
using GumpStudio.Elements;
using GumpStudio.Plugins;
using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SphereGumpExport
{
  public class SphereExporter : BasePlugin
  {
    protected DesignerForm m_Designer;
    protected MenuItem mnu_FileExportSphereExport;
    protected MenuItem mnu_MenuItem;
    protected SphereExportForm frm_SphereExportForm;

    public override string Name
    {
      get
      {
        return this.GetPluginInfo().PluginName;
      }
    }

    public override PluginInfo GetPluginInfo()
    {
      return new PluginInfo()
      {
        AuthorEmail = "mutila@gmail.com",
        AuthorName = "Mauricio Nunes",
        Description = "Exports the Gump into a Sphere script based SphereExporter by Francesco Furiani.",
        PluginName = nameof (SphereExporter),
        Version = "1.2"
      };
    }

    public override void Load(DesignerForm frmDesigner)
    {
      this.m_Designer = frmDesigner;
      this.mnu_FileExportSphereExport = new MenuItem("Sphere");
      this.mnu_FileExportSphereExport.Click += new EventHandler(this.mnu_FileExportSphereExport_Click);
      if (!this.m_Designer.mnuFileExport.Enabled)
        this.m_Designer.mnuFileExport.Enabled = true;
      this.m_Designer.mnuFileExport.MenuItems.Add(this.mnu_FileExportSphereExport);
      base.Load(frmDesigner);
    }

    private void mnu_FileExportSphereExport_Click(object sender, EventArgs e)
    {
      this.frm_SphereExportForm = new SphereExportForm(this);
      int num = (int) this.frm_SphereExportForm.ShowDialog();
    }

    public StringWriter GetSphereScript(bool bIsRevision)
    {
      StringWriter stringWriter1 = new StringWriter();
      ArrayList arrayList1 = new ArrayList();
      ArrayList arrayList2 = new ArrayList();
      stringWriter1.WriteLine("// Created {0}, with Gump Studio.", (object) DateTime.Now);
      stringWriter1.WriteLine("// Exported with with {0} ver {1}.", (object) this.GetPluginInfo().PluginName, (object) this.GetPluginInfo().Version);
      stringWriter1.WriteLine("// Script for {0}", bIsRevision ? (object) "0.56d+" : (object) "0.56c-");
      stringWriter1.WriteLine("");
      stringWriter1.WriteLine("[DIALOG {0}]", (object) this.frm_SphereExportForm.GumpName);
      StringWriter stringWriter2 = stringWriter1;
      string format = "{0}";
      int num1 = bIsRevision ? 1 : 0;
      Point location = this.m_Designer.GumpProperties.Location;
      int x = location.X;
      location = this.m_Designer.GumpProperties.Location;
      int y = location.Y;
      string str = this.Gump_WriteLocation(num1 != 0, x, y);
      stringWriter2.WriteLine(format, (object) str);
      if (!this.m_Designer.GumpProperties.Closeable)
        stringWriter1.WriteLine("{0}", bIsRevision ? (object) "NOCLOSE" : (object) "NoClose");
      if (!this.m_Designer.GumpProperties.Moveable)
        stringWriter1.WriteLine("{0}", bIsRevision ? (object) "NOMOVE" : (object) "NoMove");
      if (!this.m_Designer.GumpProperties.Disposeable)
        stringWriter1.WriteLine("{0}", bIsRevision ? (object) "NODISPOSE" : (object) "NoDispose");
      if (this.m_Designer.Stacks.Count > 0)
      {
        int id1 = 0;
        int id2 = 0;
        int id3 = 0;
        int num2 = -1;
        for (int iPage = 0; iPage < this.m_Designer.Stacks.Count; ++iPage)
        {
          stringWriter1.WriteLine("{0}", (object) this.Gump_WritePage(bIsRevision, iPage));
          GroupElement stack = this.m_Designer.Stacks[iPage] as GroupElement;
          if (stack != null)
          {
            ArrayList elementsRecursive = stack.GetElementsRecursive();
            if (elementsRecursive.Count > 0)
            {
              for (int index = 0; index < elementsRecursive.Count; ++index)
              {
                BaseElement baseElement = elementsRecursive[index] as BaseElement;
                if (baseElement != null)
                {
                  HTMLElement htmlElement = baseElement as HTMLElement;
                  if (htmlElement != null)
                  {
                    if (htmlElement.TextType == HTMLElementType.HTML)
                    {
                      if (bIsRevision)
                      {
                        string text = "HtmlGump id." + id1.ToString();
                        if (htmlElement.HTML != null)
                          arrayList2.Add((object) new SphereExporter.SphereElement(htmlElement.HTML.Length == 0 ? text : htmlElement.HTML, id1));
                        else
                          arrayList2.Add((object) new SphereExporter.SphereElement(text, id1));
                      }
                      stringWriter1.WriteLine("{0}", (object) this.Gump_WriteHTML(bIsRevision, htmlElement.X, htmlElement.Y, htmlElement.Width, htmlElement.Height, htmlElement.ShowBackground, htmlElement.ShowScrollbar, ref id1, htmlElement.HTML));
                    }
                    else
                      stringWriter1.WriteLine("{0}", (object) this.Gump_WriteXFHTML(bIsRevision, htmlElement.X, htmlElement.Y, htmlElement.Width, htmlElement.Height, htmlElement.ShowBackground, htmlElement.ShowScrollbar, htmlElement.CliLocID));
                  }
                  else
                  {
                    AlphaElement alphaElement = baseElement as AlphaElement;
                    if (alphaElement != null)
                    {
                      stringWriter1.WriteLine("{0}", (object) this.Gump_WriteCheckerTrans(bIsRevision, alphaElement.X, alphaElement.Y, alphaElement.Width, alphaElement.Height));
                    }
                    else
                    {
                      BackgroundElement backgroundElement = baseElement as BackgroundElement;
                      if (backgroundElement != null)
                      {
                        stringWriter1.WriteLine("{0}", (object) this.Gump_WriteResizePic(bIsRevision, backgroundElement.X, backgroundElement.Y, backgroundElement.Width, backgroundElement.Height, backgroundElement.GumpID));
                      }
                      else
                      {
                        ButtonElement buttonElement = baseElement as ButtonElement;
                        if (buttonElement != null)
                        {
                          arrayList1.Add((object) new SphereExporter.SphereElement("// " + buttonElement.Name + "\n// " + buttonElement.Code, buttonElement.ButtonType == ButtonTypeEnum.Reply ? buttonElement.Param : id2));
                          stringWriter1.WriteLine("{0}", (object) this.Gump_WriteButton(bIsRevision, buttonElement.X, buttonElement.Y, buttonElement.NormalID, buttonElement.PressedID, buttonElement.ButtonType == ButtonTypeEnum.Reply, buttonElement.Param, ref id2));
                        }
                        else
                        {
                          ImageElement imageElement = baseElement as ImageElement;
                          if (imageElement != null)
                          {
                            stringWriter1.WriteLine("{0}", (object) this.Gump_WriteGumpPic(bIsRevision, imageElement.X, imageElement.Y, imageElement.GumpID, imageElement.Hue.ToString()));
                          }
                          else
                          {
                            ItemElement itemElement = baseElement as ItemElement;
                            if (itemElement != null)
                            {
                              stringWriter1.WriteLine("{0}", (object) this.Gump_WriteTilePic(bIsRevision, itemElement.X, itemElement.Y, itemElement.ItemID, itemElement.Hue.ToString()));
                            }
                            else
                            {
                              LabelElement labelElement = baseElement as LabelElement;
                              if (labelElement != null)
                              {
                                if (bIsRevision)
                                {
                                  string text = "Text id." + id1.ToString();
                                  if (labelElement.Text != null)
                                    arrayList2.Add((object) new SphereExporter.SphereElement(labelElement.Text.Length == 0 ? text : labelElement.Text, id1));
                                  else
                                    arrayList2.Add((object) new SphereExporter.SphereElement(text, id1));
                                }
                                stringWriter1.WriteLine("{0}", (object) this.Gump_WriteText(bIsRevision, labelElement.X, labelElement.Y, labelElement.Hue.Index, labelElement.Text, ref id1));
                              }
                              else
                              {
                                RadioElement radioElement = baseElement as RadioElement;
                                if (radioElement != null)
                                {
                                  if (radioElement.Group != num2)
                                  {
                                    stringWriter1.WriteLine("Group{0}", bIsRevision ? (object) (" " + radioElement.Group.ToString()) : (object) ("(" + radioElement.Group.ToString() + ")"));
                                    num2 = radioElement.Group;
                                  }
                                  stringWriter1.WriteLine("{0}", (object) this.Gump_WriteRadioBox(bIsRevision, radioElement.X, radioElement.Y, radioElement.UnCheckedID, radioElement.CheckedID, radioElement.Checked, radioElement.Value));
                                }
                                else
                                {
                                  CheckboxElement checkboxElement = baseElement as CheckboxElement;
                                  if (checkboxElement != null)
                                  {
                                    stringWriter1.WriteLine("{0}", (object) this.Gump_WriteCheckBox(bIsRevision, checkboxElement.X, checkboxElement.Y, checkboxElement.UnCheckedID, checkboxElement.CheckedID, checkboxElement.Checked, ref id3));
                                  }
                                  else
                                  {
                                    TextEntryElement textEntryElement = baseElement as TextEntryElement;
                                    if (textEntryElement != null)
                                    {
                                      if (bIsRevision)
                                      {
                                        string text = "Textentry id." + textEntryElement.ID.ToString();
                                        if (textEntryElement.InitialText != null)
                                          arrayList2.Add((object) new SphereExporter.SphereElement(textEntryElement.InitialText.Length == 0 ? text : textEntryElement.InitialText, id1));
                                        else
                                          arrayList2.Add((object) new SphereExporter.SphereElement(text, id1));
                                      }
                                      stringWriter1.WriteLine("{0}", (object) this.Gump_WriteTextEntry(bIsRevision, textEntryElement.X, textEntryElement.Y, textEntryElement.Width, textEntryElement.Height, textEntryElement.Hue.ToString(), textEntryElement.InitialText, textEntryElement.ID, ref id1));
                                    }
                                    else
                                    {
                                      TiledElement tiledElement = baseElement as TiledElement;
                                      if (tiledElement != null)
                                        stringWriter1.WriteLine("{0}", (object) this.Gump_WriteGumpPicTiled(bIsRevision, tiledElement.X, tiledElement.Y, tiledElement.Width, tiledElement.Height, tiledElement.GumpID));
                                    }
                                  }
                                }
                              }
                            }
                          }
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
      stringWriter1.WriteLine("");
      if (!bIsRevision)
      {
        stringWriter1.WriteLine("[DIALOG {0} text]", (object) this.frm_SphereExportForm.GumpName);
        foreach (SphereExporter.SphereElement sphereElement in arrayList2)
          stringWriter1.WriteLine("{0}", (object) sphereElement.sText);
        stringWriter1.WriteLine("");
      }
      stringWriter1.WriteLine("[DIALOG {0} button]", (object) this.frm_SphereExportForm.GumpName);
      foreach (SphereExporter.SphereElement sphereElement in arrayList1)
      {
        stringWriter1.WriteLine("ON={0}", (object) sphereElement.iId.ToString());
        stringWriter1.WriteLine("{0}", (object) sphereElement.sText);
        stringWriter1.WriteLine("");
      }
      stringWriter1.WriteLine("{0}", (object) this.Gump_WriteEOF());
      return stringWriter1;
    }

    private string Gump_WriteEOF()
    {
      return "\n" + "[EOF]";
    }

    private string Gump_WriteLocation(bool bIsRevision, int x, int y)
    {
     // if (bIsRevision)
        return x.ToString() + "," + y.ToString();
      //return "SetLocation=" + x.ToString() + "," + y.ToString();
    }

    private string Gump_WritePage(bool bIsRevision, int iPage)
    {
      //if (bIsRevision)
        return "page " + iPage.ToString();
      //return "Page(" + iPage.ToString() + ")";
    }

    private string Gump_WriteHTML(bool bIsRevision, int x, int y, int width, int height, bool background, bool scrollbar, ref int id, string text)
    {
      StringWriter stringWriter = new StringWriter();
      //if (bisrevision)
      //{
        stringWriter.Write("htmlgump {0} {1} {2} {3} {4} {5} {6}", (object) x.ToString(), (object) y.ToString(), (object) width.ToString(), (object) height.ToString(), (object) id.ToString(), background ? (object) "1" : (object) "0", scrollbar ? (object) "1" : (object) "0");
        ++id;
      //}
      //else
      //{
      //  if (text == null)
      //    text = " ";
      //  string str = text.ToString();
      //  str.Replace("\"", "\\\"");
      //  stringWriter.Write("HtmlGumpA({0},{1},{2},{3},\"{4}\",{5},{6})", (object) x.ToString(), (object) y.ToString(), (object) width.ToString(), (object) height.ToString(), (object) str, background ? (object) "1" : (object) "0", scrollbar ? (object) "1" : (object) "0");
      //}
      return stringWriter.ToString();
    }

    private string Gump_WriteXFHTML(bool bIsRevision, int x, int y, int width, int height, bool background, bool scrollbar, int cliloc)
    {
      StringWriter stringWriter = new StringWriter();
      //if (bIsRevision)
        stringWriter.Write("xmfhtmlgump {0} {1} {2} {3} {4} {5} {6}", (object) x.ToString(), (object) y.ToString(), (object) width.ToString(), (object) height.ToString(), (object) cliloc.ToString(), background ? (object) "1" : (object) "0", scrollbar ? (object) "1" : (object) "0");
      //else
      //  stringWriter.Write("XmfHtmlGump({0},{1},{2},{3},{4},{5},{6})", (object) x.ToString(), (object) y.ToString(), (object) width.ToString(), (object) height.ToString(), (object) cliloc.ToString(), background ? (object) "1" : (object) "0", scrollbar ? (object) "1" : (object) "0");
      return stringWriter.ToString();
    }

    private string Gump_WriteCheckerTrans(bool bIsRevision, int x, int y, int width, int height)
    {
      StringWriter stringWriter = new StringWriter();
//    if (bIsRevision)
        stringWriter.Write("checkertrans {0} {1} {2} {3}", (object) x.ToString(), (object) y.ToString(), (object) width.ToString(), (object) height.ToString());
      //else
      //  stringWriter.Write("CheckerTrans({0},{1},{2},{3})", (object) x.ToString(), (object) y.ToString(), (object) width.ToString(), (object) height.ToString());
      return stringWriter.ToString();
    }

    private string Gump_WriteResizePic(bool bIsRevision, int x, int y, int width, int height, int gumpid)
    {
      StringWriter stringWriter = new StringWriter();
      //if (bIsRevision)
        stringWriter.Write("resizepic {0} {1} {2} {3} {4}", (object) x.ToString(), (object) y.ToString(), (object) gumpid.ToString(), (object) width.ToString(), (object) height.ToString());
     // else
       // stringWriter.Write("ResizePic({0},{1},{2},{3},{4})", (object) x.ToString(), (object) y.ToString(), (object) gumpid.ToString(), (object) width.ToString(), (object) height.ToString());
      return stringWriter.ToString();
    }

    private string Gump_WriteGumpPic(bool bIsRevision, int x, int y, int gumpid, string hue)
    {
      StringWriter stringWriter = new StringWriter();
      bool flag = false;
      if (hue != null && hue.Length != 0 && string.Compare(hue, "0", true) != 0)
        flag = true;
     // if (bIsRevision)
        stringWriter.Write("gumppic {0} {1} {2}{3}", (object) x.ToString(), (object) y.ToString(), (object) gumpid.ToString(), flag ? (object) (" " + hue) : (object) "");
    //  else
    //    stringWriter.Write("GumpPic({0},{1},{2}{3})", (object) x.ToString(), (object) y.ToString(), (object) gumpid.ToString(), flag ? (object) ("," + hue) : (object) "");
      return stringWriter.ToString();
    }

    private string Gump_WriteTilePic(bool bIsRevision, int x, int y, int itemid, string hue)
    {
      StringWriter stringWriter = new StringWriter();
      bool flag = false;
      if (hue != null && hue.Length != 0 && string.Compare(hue, "0", true) != 0)
        flag = true;
    //  if (bIsRevision)
        stringWriter.Write("tilepic{0} {1} {2} {3}{4}", flag ? (object) nameof (hue) : (object) "", (object) x.ToString(), (object) y.ToString(), (object) itemid.ToString(), flag ? (object) (" " + hue) : (object) "");
    //  else
   //     stringWriter.Write("TilePic{0}({1},{2},{3}{4})", flag ? (object) "Hue" : (object) "", (object) x.ToString(), (object) y.ToString(), (object) itemid.ToString(), flag ? (object) ("," + hue) : (object) "");
      return stringWriter.ToString();
    }

    private string Gump_WriteGumpPicTiled(bool bIsRevision, int x, int y, int width, int height, int gumpid)
    {
      StringWriter stringWriter = new StringWriter();
   //   if (bIsRevision)
        stringWriter.Write("gumppictiled {0} {1} {2} {3} {4}", (object) x.ToString(), (object) y.ToString(), (object) width.ToString(), (object) height.ToString(), (object) gumpid.ToString());
   //   else
    //    stringWriter.Write("GumpPicTiled({0},{1},{2},{3},{4})", (object) x.ToString(), (object) y.ToString(), (object) width.ToString(), (object) height.ToString(), (object) gumpid.ToString());
      return stringWriter.ToString();
    }

    private string Gump_WriteButton(bool bIsRevision, int x, int y, int normalid, int pressedid, bool isReply, int pageto, ref int id)
    {
      StringWriter stringWriter = new StringWriter();
  //    if (bIsRevision)
        stringWriter.Write("button {0} {1} {2} {3} {4} {5} {6}", (object) x.ToString(), (object) y.ToString(), (object) normalid.ToString(), (object) pressedid.ToString(), (object) "1", isReply ? (object) "0" : (object) pageto.ToString(), isReply ? (object) pageto.ToString() : (object) id.ToString());
  //    else
  //      stringWriter.Write("Button({0},{1},{2},{3},{4},{5},{6})", (object) x.ToString(), (object) y.ToString(), (object) normalid.ToString(), (object) pressedid.ToString(), (object) "1", isReply ? (object) "0" : (object) pageto.ToString(), isReply ? (object) pageto.ToString() : (object) id.ToString());
      if (!isReply)
        ++id;
      return stringWriter.ToString();
    }

    private string Gump_WriteText(bool bIsRevision, int x, int y, int hue, string text, ref int id)
    {
      StringWriter stringWriter = new StringWriter();
      if (bIsRevision)
      {        
        stringWriter.Write("dtext {0} {1} {2} {3}", (object)x.ToString(), (object)y.ToString(), (object)hue, (object)text);
        ++id;
    }
      else
      {
        stringWriter.Write("text {0} {1} {2} {3}", (object)x.ToString(), (object)y.ToString(), (object)hue, (object)id.ToString());
        ++id;
      }
      return stringWriter.ToString();
    }

    private string Gump_WriteCheckBox(bool bIsRevision, int x, int y, int uncheckedid, int checkedid, bool ischecked, ref int id)
    {
      StringWriter stringWriter = new StringWriter();
      //if (bIsRevision)
        stringWriter.Write("checkbox {0} {1} {2} {3} {4} {5}", (object) x.ToString(), (object) y.ToString(), (object) checkedid.ToString(), (object) uncheckedid.ToString(), ischecked ? (object) "1" : (object) "0", (object) id.ToString());
     // else
       // stringWriter.Write("CheckBox({0},{1},{2},{3},{4},{5})", (object) x.ToString(), (object) y.ToString(), (object) checkedid.ToString(), (object) uncheckedid.ToString(), ischecked ? (object) "1" : (object) "0", (object) id.ToString());
      ++id;
      return stringWriter.ToString();
    }

    private string Gump_WriteTextEntry(bool bIsRevision, int x, int y, int width, int height, string hue, string text, int returnid, ref int id)
    {
      StringWriter stringWriter = new StringWriter();
     // if (bIsRevision)
     // {
        stringWriter.Write("textentry {0} {1} {2} {3} {4} {5} {6}", (object) x.ToString(), (object) y.ToString(), (object) width.ToString(), (object) height.ToString(), (object) hue, (object) returnid.ToString(), (object) id.ToString());
        ++id;
     // }
     // else
     // {
      //  if (text == null)
      //    text = " ";
     //   string str = text.ToString();
     //   str.Replace("\"", "\\\"");
     //   stringWriter.Write("TextEntryA({0},{1},{2},{3},{4},{5},\"{6}\")", (object) x.ToString(), (object) y.ToString(), (object) width.ToString(), (object) height.ToString(), (object) hue, (object) returnid.ToString(), (object) str);
     // }
      return stringWriter.ToString();
    }

    private string Gump_WriteRadioBox(bool bIsRevision, int x, int y, int uncheckedid, int checkedid, bool ischecked, int id)
    {
      StringWriter stringWriter = new StringWriter();
     // if (bIsRevision)
        stringWriter.Write("radio {0} {1} {2} {3} {4} {5}", (object) x.ToString(), (object) y.ToString(), (object) checkedid.ToString(), (object) uncheckedid.ToString(), ischecked ? (object) "1" : (object) "0", (object) id.ToString());
    //  else
    //    stringWriter.Write("Radio({0},{1},{2},{3},{4},{5})", (object) x.ToString(), (object) y.ToString(), (object) checkedid.ToString(), (object) uncheckedid.ToString(), ischecked ? (object) "1" : (object) "0", (object) id.ToString());
      return stringWriter.ToString();
    }

    private class SphereElement
    {
      public string sText;
      public int iId;

      public SphereElement(string text, int id)
      {
        this.sText = text;
        this.iId = id;
      }
    }
  }
}
