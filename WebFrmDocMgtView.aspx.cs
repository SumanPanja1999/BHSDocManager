﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BPWEBAccessControl
{
   
    public partial class DocMgtView : System.Web.UI.Page
    {
        #region Form Event
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.Page.Title = BPWEBAccessControl.Global.PageTitle;
            Master.masterlblFormName.Text = "Document View";
            this.lblfrmName.Text = "Document View";
            if ((int)Session["DEAFULT_LOGIN"] == 0)
            {
                Page.Response.Redirect("default.aspx");
                return;
            }
            else
            {
                if ((int)Session["LOGIN_STATUS"] == 0)
                {
                    Page.Response.Redirect("default.aspx");
                    return;
                }
                //DisplayUserName();
            }
            //----------------
            //HideLog();
            try
            {
                if (Page.IsPostBack == true)
                {
                    // 
                }
                else
                {

                    Session["VERIFICATION_STATE"] = 0;
                    LoadDynamicData();
                    loadLanguageOnLabel();
                    //Cancel();

                }
               
            }
            catch (System.Exception ex)
            {
                // ShowLog("Error: \n" + ex.Message.ToString());
            }
            finally
            {
            }
        }
        #endregion

        #region Dialog Function
        private void displayMsgs(string textMsg, string type, string FLAG)
        {
            try
            {
                this.dlgMsg.Text = textMsg;
                
                if (type.Trim().ToUpper() == "OK")
                {
                    this.dlgImage.ImageUrl = "Picture/not_found.png";
                    this.dlgOk.Visible = true;
                   
                    this.dlgMsg.ForeColor = System.Drawing.Color.Green;
                }
                
                
                
                this.mvwDataVw.SetActiveView(this.vw02);

            }
            catch (Exception ex)
            {
                ShowLog("Error: \n" + ex.Message.ToString());
            }
            finally
            {
            }
        }//eof
        protected void dlgOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.lblDlgState.Text = "TRUE";
                dialogFunction();
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.ClearContent();
                System.Web.HttpContext.Current.Response.ClearHeaders();
            }
            catch (Exception ex)
            {
                ShowLog("Error: \n" + ex.Message.ToString());
            }
            finally
            {
            }
        }//eof
        private void dialogFunction()
        {
            
            string vwName = "vw02";
            
            try
            {
				this.lblDlgState.Text = "";
                this.mvwDataVw.ActiveViewIndex = returnView(vwName);
            }
            catch (Exception ex)
            {
                ShowLog("Error: \n" + ex.Message.ToString());
            }
            finally
            {
            }
        }//eof
        private int returnView(string vwId)
        {
            string vwIds = "";
            int vwNo = 2;
            try
            {
                foreach (View vw in mvwDataVw.Views)
                {
                    vwIds = vw.ID.ToString().ToUpper().Trim();
                    if (string.Compare(vwId.ToUpper().Trim(), vwIds.Trim(), false) == 0)
                    {
                        break;
                    }
                    vwNo++;
                }
            }
            catch (Exception ez)
            {
                ShowLog("Error: \n" + ez.Message.ToString());
            }
            finally
            {
            }

            return vwNo;
        }//eof

        #endregion

        #region commom Function
        public void ShowLog(string strMessage)
        {
            //TxtMsgBox.Visible = true;
            //TxtMsgBox.Text = strMessage;
        }//enf of function
        private void loadLanguageOnLabel()
        {

            System.Data.DataTable dtLocal = null;
            System.Data.DataTable dtTemp = null;
            try
            {
                dtLocal = PWOMS.clsSysLanguage.sysLanguage();
                if (dtLocal != null)
                {
                    dtLocal.DefaultView.RowFilter = "ScreenName='Application'";
                    dtLocal.DefaultView.RowStateFilter = DataViewRowState.CurrentRows;
                    dtTemp = dtLocal.DefaultView.ToTable();
                    if (dtTemp.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtTemp.Rows)
                        {
                            System.Web.UI.WebControls.Label lblCtrl =
                            (Label)(this.dataUpdatePanel.FindControl(dr["CtrlID"].ToString().Trim()));

                            lblCtrl.Text = dr["LNG"].ToString().Trim();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowLog("Error in Loading Language : " + ex.Message.ToString().Trim());
            }
            finally
            {
                dtLocal = null;
            }
        }//eof       

        #endregion

        #region Load TreeView Data
        private void LoadDynamicData()
        {
            System.Data.DataSet dsLocal = null;
            PWOMS.clsDocApplication objApp = null;
            //string strSiteId = Session["USER_SITE"].ToString().Trim();
            try
            {
                objApp = new PWOMS.clsDocApplication();
                objApp.GetAllData( out dsLocal);

                if (dsLocal != null && dsLocal.Tables.Count > 0)
                {
                    DataTable table = dsLocal.Tables[0];
                    DataView view = new DataView(table);
                    Dictionary<string, TreeNode> groupNodes = new Dictionary<string, TreeNode>();
                    foreach (DataRowView row in view)
                    {
                        string entryId = row["EntryID"].ToString();
                        string groupName = row["Documents_Group"].ToString();
                        string docName = row["DocumentName"].ToString();

                        if (!groupNodes.ContainsKey(groupName))
                        {
                            TreeNode groupNode = new TreeNode(groupName);
                            TreeView1.Nodes.Add(groupNode);
                            groupNodes[groupName] = groupNode;
                        }

                        // Add the header as a child node under the corresponding group node
                        TreeNode headerNode = new TreeNode(docName); // Setting Value to NodeID
                        headerNode.Value = entryId;
                        groupNodes[groupName].ChildNodes.Add(headerNode);
                    }
                    TreeView1.ExpandAll();
                }
            }
            catch (System.Exception ex)
            {
                ShowLog("Error: \n" + ex.Message.ToString());
            }
            finally
            {
                objApp = null;
                dsLocal = null;
            }
        }

            #endregion

        #region Load Document Details
            protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
            {
            TreeNode selectedNode = TreeView1.SelectedNode;
            if (selectedNode != null)
            {
                string nodeId = selectedNode.Value; // This will now contain the NodeID
                DataTable dtDocument = GetDocumentDetails(nodeId);
                SetDoucmentDetails(dtDocument);
            }
            }

        protected void SetDoucmentDetails(DataTable dtDocument)
        {
            if (dtDocument != null && dtDocument.Rows.Count > 0)
            {
                DataRow docRow = dtDocument.Rows[0];

                lblDocDESC.Text = docRow["DocumentDescription"].ToString();

                lblVNo.Text = docRow["VersionNo"].ToString();
                lblBNo.Text = docRow["BuildNo"].ToString();
                lblHeader.Text = docRow["Header"].ToString();
                dvSec1.InnerHtml = Server.HtmlDecode("" + docRow["Section1"].ToString());
                dvSec2.InnerHtml = Server.HtmlDecode("" + docRow["Section2"].ToString());
                dvCon1.InnerHtml = Server.HtmlDecode("" + docRow["Content1"].ToString());
                dvCon2.InnerHtml = Server.HtmlDecode("" + docRow["Content2"].ToString());
                if (!string.IsNullOrEmpty(docRow["FilePath1"].ToString()))
                {
                    btnDownload1.Visible = true;
                    btnDownload1.Text = Path.GetFileName(docRow["FilePath1"].ToString());
                }
                else
                {
                    btnDownload1.Visible = false;
                }
                
                if (!string.IsNullOrEmpty(docRow["FilePath2"].ToString()))
                {
                    btnDownload2.Visible = true;
                    btnDownload2.Text = Path.GetFileName(docRow["FilePath2"].ToString());
                }
                else
                {
                    btnDownload2.Visible = false;
                }

                if (!string.IsNullOrEmpty(docRow["FilePath3"].ToString()))
                {
                    btnDownload3.Visible = true;
                    btnDownload3.Text = Path.GetFileName(docRow["FilePath3"].ToString());
                }
                else
                {
                    btnDownload3.Visible = false;
                }

                lblFooter.Text = docRow["Footer"].ToString();
                Show_Content();//Show View After Click any node 
            }
        }


        private DataTable GetDocumentDetails(string nodeId)
        {
            System.Data.DataSet dsLocal = null;
            PWOMS.clsDocApplication objApp = null;
            DataTable dtDocument = new DataTable();
            try
            {
                objApp = new PWOMS.clsDocApplication();
                objApp.GetAllData(out dsLocal);
 
                if (dsLocal != null && dsLocal.Tables.Count > 0)
                {
                    dtDocument = dsLocal.Tables[0];

                    if (!string.IsNullOrEmpty(nodeId))
                    {
                        DataView dvLocal = new DataView(dtDocument); // Create a DataView to filter the DataTable
                        dvLocal.RowFilter = "EntryID = '" + nodeId.Replace("'", "''") + "'"; // Apply the filter
                        dtDocument = dvLocal.ToTable(); // Convert the filtered DataView back to a DataTable
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objApp = null;
            }

            return dtDocument;
        }//eof

        public void Show_Content()
		{
            sectionDetailsContainer.Style["display"] = "block";
        }//eof
     
        #endregion

        #region File Download Related
        private void GetFileName(string btnName)
        {
            string filePath = "";
            TreeNode selectedNode = TreeView1.SelectedNode;
			
                if (selectedNode != null)
                {
                    string docID = selectedNode.Value;
                    DataTable dtDocument = GetDocumentDetails(docID);
                    if (dtDocument != null && dtDocument.Rows.Count > 0)
                    {
                        DataRow docRow = dtDocument.Rows[0];
                        if (btnName == "btnDownload1")
                        {
                            btnDownload1.Text = docRow["FilePath1"].ToString();
                            filePath = btnDownload1.Text;
                        if (filePath != null)
                        {
                           
                            btnDownload1.Text = filePath;
                        }
                           
                        }
                        else if (btnName == "btnDownload2")
                        {
                            btnDownload2.Text = docRow["FilePath2"].ToString();
                            filePath = btnDownload2.Text;
                            btnDownload2.Text = "Download File 2";
                        }
                        else if (btnName == "btnDownload3")
                        {
                            btnDownload3.Text = docRow["FilePath3"].ToString();
                            filePath = btnDownload3.Text;
                            btnDownload3.Text = "Download File 3";
                        }
                    }
                }

                if (!string.IsNullOrEmpty(filePath))
                {

                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(filePath));
                    Response.TransmitFile(filePath);
                    Response.End();
                    
                }
                else
                {
                   displayMsgs("File Not Found!", "Ok", "Save");
                   
                }
           
			
			
            
        }

       
        protected void btnDownload1_Click(object sender, EventArgs e)
        {
            GetFileName("btnDownload1");
            
           
        }//eof

        protected void btnDownload2_Click(object sender, EventArgs e)
        {
            GetFileName("btnDownload2");
        }//eof

        protected void btnDownload3_Click(object sender, EventArgs e)
        {
            GetFileName("btnDownload3");
        }//eof


        #endregion

        #region Search Document view Related

        protected void btnSearchView_Click(object sender,EventArgs e)
		{
            try
            {
                this.lblViewName.Text = this.mvwDataVw.GetActiveView().ID.ToString();
                LoadData(true, "", "DOCVIEW");
                this.tbValue.Text = "";
                this.lblSearchTitle.Text = "Search : DOCVIEW";
                this.btnCancelSearch.Visible = true; //set as false in Cancel() function; if the search screen is the first screen
                this.mvwDataVw.SetActiveView(this.vw00);
            }
            catch (Exception ex)
            {
                ShowLog("Error: \n" + ex.Message.ToString());
            }
            finally
            {
            }
            
        }

        private void LoadData(bool IsLoad, string strKey, string FLAG)
        {

            string SB = "";
            PWOMS.clsDocApplication objApp = null;
            System.Data.DataSet dsLocal = null;
            System.Data.DataTable dtLocal = null;
            System.Data.DataView dvLocal = null;
            this.panSearch.Visible = false;
            this.dgSearch.DataSource = null;
            this.dgSearch.Visible = false;
            this.lblInfoDox.Text = "";
            int rowNo = 0;
            string strSiteId = Session["USER_SITE"].ToString().Trim();

            string fromDate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string toDate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            try
            {
                objApp = new PWOMS.clsDocApplication();
                if (FLAG == "") //this is default search function
                {
                    //objApp.SearchTeleData(fromDate, toDate, strKey,strSiteId, out dsLocal);
                }
                if (FLAG == "DOCVIEW")
                {
                    objApp.SearchDocument(fromDate, toDate, strKey, strSiteId, out dsLocal);
                }
                this.lblViewState.Text = FLAG;
                //Make the 1/1/1901 blank

                dtLocal = dsLocal.Tables[0];

                LoadViewScreenInformation(ref dtLocal, "", IsLoad, FLAG);                       // Show all the column name of tbl employee in SearchBy Drop down menu

                dvLocal = new DataView();
                dvLocal.Table = dtLocal;
                this.dgSearch.DataSource = dvLocal;

                if (dvLocal.Count > 0)
                {
                    this.dgSearch.Visible = true;
                    this.dgSearch.DataBind();
                    this.panSearch.Visible = true;
                    rowNo = dvLocal.Count;
                    SB = SB + rowNo + " Record(s) found";
                    this.lblInfoDox.Text = SB;
                }
                else
                {
                    SB = "No record found....................";
                    this.lblInfoDox.Text = SB;
                }

            }
            catch (System.Exception ex)
            {
                ShowLog("Error: \n" + ex.Message.ToString());
            }
            finally
            {

            }
        }//eof
        private void LoadViewScreenInformation(ref DataTable dtFind, string strExcludeColums, bool IsLoad, string FLAG)
        {
            try
            {
                if (IsLoad == true)
                {
                    this.ddlSearchBy.DataSource = GetTableDefination(ref dtFind);
                    this.ddlSearchBy.DataTextField = "EntryID";
                    this.ddlSearchBy.DataValueField = "EntryID";
                    this.ddlSearchBy.DataBind();
                    this.ddlSearchBy.SelectedIndex = 1;

                    //if (FLAG=="PAY")
                    //{
                    //    this.ddlSearchBy.SelectedValue = "Supplier";
                    //}

                }
            }
            catch (System.Exception ex)
            {
                ShowLog("Error: \n" + ex.Message.ToString());
            }
            finally
            {
                //
            }
        } //eof
        private DataTable GetTableDefination(ref DataTable dtFind)                                  // Get all The Column Name of tblDocMgt
        {

            DataTable dt = new DataTable("tblSearchKeyList");
            dt.Columns.Add("EntryID", typeof(String));

            for (Int32 i = 0; i < dtFind.Columns.Count /*= 4*/; i++)
            {
                if (dtFind.Columns[i].DataType == typeof(System.String) ||
                    dtFind.Columns[i].DataType == typeof(System.Char))
                {
                    //if (dtFind.Columns[i].ColumnName.ToString().Length >= 4)
                    //{
                    //    if (dtFind.Columns[i].ColumnName.ToString().Substring(dtFind.Columns[i].ColumnName.ToString().Length - 4, 4).ToUpper() == "DATE")
                    //    {//do nothing
                    //    }
                    //    else
                    //    {
                    //        dt.Rows.Add(new Object[] { dtFind.Columns[i].ColumnName.ToString() });
                    //    }
                    //}
                    //else
                    //{
                    dt.Rows.Add(new Object[] { dtFind.Columns[i].ColumnName.ToString() });
                    //}
                }
            }
            dt.AcceptChanges();
            return dt;
        } //eof
        

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string stringKey = "";
            string FLAG = "";
            try
            {

                if (this.tbValue.Text.Trim() != "")
                {
                    if (this.ddlSearchBy.SelectedValue.Trim() != "")
                    {
                        FLAG = this.lblViewState.Text.Trim();
                        stringKey = this.ddlSearchBy.SelectedValue.Trim() + " like '%" + this.tbValue.Text.Trim() + "%'";                                                   //*********//
                    }
                    LoadData(false, stringKey, FLAG);
                }
            }
            catch (Exception ex)
            {
                ShowLog("Error: \n" + ex.Message.ToString());
            }
            finally
            {
            }
        }//eof
        protected void btnCancelSearch_Click(object sender, EventArgs e)
        {
            this.mvwDataVw.SetActiveView(this.vw02);
        }//eof
        public void Grid_Command(object sender, DataGridCommandEventArgs e)
        {
            //string vwIndex = this.lblViewName.Text;
            string DOCID = "";
            //string CollectionID = "";
            if (((LinkButton)e.CommandSource).CommandName != "Page")
            {

                TableCell DOCIDCell = e.Item.Cells[1];
                //TableCell CollectionIDCell = e.Item.Cells[2];
                DOCID = DOCIDCell.Text;
               // CollectionID = CollectionIDCell.Text;
            }
            if (((LinkButton)e.CommandSource).CommandName == "ViewDoc")
            {
                
                if (this.lblViewState.Text.Trim() == "DOCVIEW")
                {
                    
                    string EntryID = DOCID;
                    DataTable dtDocument = GetDocumentDetails(EntryID);
                    SetDoucmentDetails(dtDocument);

                }
                this.mvwDataVw.SetActiveView(this.vw02);
            }

        }//eof 

        #endregion

    }

}
