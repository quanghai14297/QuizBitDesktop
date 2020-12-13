using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Desktop.BL;
using System.IO;
using Desktop.Entity;

namespace ClientApp.UI.Controls
{
    public partial class UctFood : UserControl
    {
        public UctFood()
        {
            InitializeComponent();
        }

        public UctFood(DictionaryDataSet.InventoryItemRow itemRow)
        {
            InitializeComponent();
            DataInventoryItemRow = itemRow;
        }

        #region Override Click

        private void lblInventoryName_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void lblUnitPrice_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void lblFillBackground_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        #endregion

        #region Property

        private decimal _unitPrice;

        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                _unitPrice = value;
                lblUnitPrice.Text = CommonFunction.ConvertToCurrency(_unitPrice);
            }
        }

        private Guid _inventoryItemID;

        /// <summary>
        /// Mã mặt hàng
        /// </summary>
        public Guid InventoryItemID
        {
            get { return _inventoryItemID; }
            set
            {
                _inventoryItemID = value;
            }
        }

        private string _inventoryItemName;

        /// <summary>
        /// Tên mặt hàng
        /// </summary>
        public string InventoryItemName
        {
            get { return _inventoryItemName; }
            set
            {
                _inventoryItemName = value;
                lblInventoryName.Text = _inventoryItemName;
                ttpManager.SetUltraToolTip(this, new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo(_inventoryItemName, Infragistics.Win.ToolTipImage.None, null, Infragistics.Win.DefaultableBoolean.Default));
                ttpManager.SetUltraToolTip(lblFillBackground, new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo(_inventoryItemName, Infragistics.Win.ToolTipImage.None, null, Infragistics.Win.DefaultableBoolean.Default));
            }
        }

        private EnumInventoryItemType _inventoryItemType;

        /// <summary>
        /// Loại mặt hàng
        /// </summary>
        public EnumInventoryItemType InventoryItemType
        {
            get { return _inventoryItemType; }
            set { _inventoryItemType = value; }
        }

        /// <summary>
        /// Load dữ liệu mặt hàng
        /// </summary>
        public void LoadInventory()
        {
            var item = ShareDictionary.DsDictionary.InventoryItem.FindByInventoryItemID(InventoryItemID);
            if (item != null)
            {
                if (!item.IsFileResourceNull())
                {
                    MemoryStream mem = new MemoryStream(item.FileResource);
                    pnlBackground.Appearance.ImageBackground = Image.FromStream(mem);
                }
            }
        }

        private DictionaryDataSet.InventoryItemRow _dataInventoryItemRow;

        /// <summary>
        /// Dữ liệu mặt hàng
        /// </summary>
        public DictionaryDataSet.InventoryItemRow DataInventoryItemRow
        {
            get { return _dataInventoryItemRow; }
            set
            {
                _dataInventoryItemRow = value;
                if (_dataInventoryItemRow != null)
                {
                    if (!_dataInventoryItemRow.IsFileResourceNull())
                    {
                        MemoryStream mem = new MemoryStream(_dataInventoryItemRow.FileResource);
                        pnlBackground.Appearance.ImageBackground = Image.FromStream(mem);
                    }
                    UnitPrice = _dataInventoryItemRow.IsUnitPriceNull() ? 0 : _dataInventoryItemRow.UnitPrice;
                    InventoryItemName = _dataInventoryItemRow.InventoryItemName;
                    InventoryItemType = (EnumInventoryItemType)_dataInventoryItemRow.InventoryItemType;
                }
            }
        }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UctFood_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }
    }
}
