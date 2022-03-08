﻿using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Quiz
{
    public partial class CategoryMaster : Form
    {
        public CategoryMaster()
        {
            InitializeComponent();
        }

        #region Form Events
        private void CategoryMaster_Load(object sender, EventArgs e)
        {
            Populate();
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// This method Populated the Grid View with data from CategoryMaster Table
        /// </summary>
        private void Populate()
        {
            dgv_CategoryMaster.Rows.Clear();
            DataSet masterData = DataLayer.GetCategoryMaster();
            foreach (DataRow row in masterData.Tables[0].Rows)
            {
                dgv_CategoryMaster.Rows.Add();
                dgv_CategoryMaster.Rows[dgv_CategoryMaster.Rows.Count - 1].Cells[0].Value = row["Id"];
                dgv_CategoryMaster.Rows[dgv_CategoryMaster.Rows.Count - 1].Cells[1].Value = row["CategoryName"];
                PrepareRow(dgv_CategoryMaster.CurrentRow.Index);
            }
            dgv_CategoryMaster.Rows.Add();
            PrepareRow(dgv_CategoryMaster.CurrentRow.Index);
        }
        /// <summary>
        /// This method Prepopulates Button text in a Row
        /// </summary>
        /// <param>
        /// <c>index</c> The index of the row to be processed
        /// </param>
        private void PrepareRow(int index)
        {

            dgv_CategoryMaster.Rows[index].Cells[2].Value = "Edit";
            dgv_CategoryMaster.Rows[index].Cells[3].Value = "Delete";

        }
        /// <summary>
        /// This method Diables or Enables Editing of a given Row
        /// </summary>
        /// <param>
        /// <c>row</c> is the Index of Perform the Action on.
        /// </param>
        ///  /// <param>
        /// <c>Disable</c> contains value for Disble/Enable.
        /// </param>
        private void DisableEdit(int row, bool Disable)
        {
            if (!Disable && (row == dgv_CategoryMaster.Rows.Count - 1))
            {
                dgv_CategoryMaster.Rows.Add();
                PrepareRow(dgv_CategoryMaster.Rows.Count - 1);
            }
            dgv_CategoryMaster.Rows[row].Cells[1].ReadOnly = Disable;
            dgv_CategoryMaster.Rows[row].Cells[1].Style.BackColor = Disable ? Color.White : Color.Tan;

        }
        /// <summary>
        /// This method Deletes a Row from the Data Grid View.
        /// </summary>
        /// <param>
        /// <c>index</c> is index of the row to be deleted.
        /// </param>
        private void DeleteRow(int index)
        {
            dgv_CategoryMaster.Rows.RemoveAt(index);
        }
        /// <summary>
        /// This method Validates the GridView on Sumbit.
        /// </summary>
        /// <param>
        private bool validateGridView()
        {
            foreach (DataGridViewRow row in dgv_CategoryMaster.Rows)
            {

                if (row.Cells[1].Value == null || row.Cells[1].Value.ToString() == string.Empty)
                {
                    MessageBox.Show("Enter Value of Category at Row " + (row.Index + 1), "Validation Failed");
                    row.Cells[1].Style.BackColor = Color.Red;
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region Button Events
        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (validateGridView())
            {
                DataLayer.clearData("CategoryMaster");
                foreach (DataGridViewRow row in dgv_CategoryMaster.Rows)
                {
                    DataLayer.InsertCategoryMasterData(row);
                }
                Populate();
                MessageBox.Show("Sucessfully Saved Data", "Success");

            }
        }

        #endregion

        #region Data Grid View Events
        private void dgv_CategoryMaster_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            DisableEdit(e.RowIndex, true);
        }
        private void dgv_CategoryMaster_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 2: DisableEdit(dgv_CategoryMaster.CurrentCell.RowIndex, false); break;
                case 3: DeleteRow(dgv_CategoryMaster.CurrentCell.RowIndex); break;
            }
        }
        #endregion

    }
}
