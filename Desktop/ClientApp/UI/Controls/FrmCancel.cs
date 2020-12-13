using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp.UI.Controls
{
    public partial class FrmCancel : Form
    {
        public FrmCancel()
        {
            InitializeComponent();
        }


        private EnumCancelAction _cancelAction = EnumCancelAction.Booking;

        public EnumCancelAction CancelAction
        {
            get
            {
                return _cancelAction;
            }
            set
            {
                _cancelAction = value;
                Text = "Hủy " + (_cancelAction == EnumCancelAction.Booking ? "Đặt bàn" : "Order");
            }
        }

        /// <summary>
        /// Lý do hủy
        /// </summary>
        public string CancelDescription
        {
            get
            {
                return txtDescription.Text;
            }
            set
            {
                txtDescription.Text = value;
            }
        }


        private void FrmCancel_Load(object sender, EventArgs e)
        {

        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Yes;
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.No;
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }
    }
}
