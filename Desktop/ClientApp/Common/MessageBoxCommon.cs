using ClientApp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    /// <summary>
    /// Lớp show message
    /// </summary>
    public class MessageBoxCommon
    {
        /// <summary>
        /// Hàm Show Exception
        /// </summary>
        /// <param name="ex"></param>
        public static void ShowException(Exception ex)
        {
            MessageBox.Show(ex.Message, Constant.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Hàm Show Message
        /// </summary>
        /// <param name="ex"></param>
        public static void ShowMessage(string message)
        {
            MessageBox.Show(message, Constant.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Hàm Show Message Lỗi
        /// </summary>
        /// <param name="ex"></param>
        public static void ShowMessageError(string message)
        {
            MessageBox.Show(message, Constant.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Hàm Show Cảnh báo
        /// </summary>
        /// <param name="ex"></param>
        public static void ShowExclamation(string message)
        {
            MessageBox.Show(message, Constant.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Hàm Show câu hỏi YesNo
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static DialogResult ShowYesNoQuestion(string message)
        {
            return MessageBox.Show(message, Constant.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        /// <summary>
        /// Hàm Show câu hỏi YesNoCancel
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static DialogResult ShowYesNoCancelQuestion(string message)
        {
            return MessageBox.Show(message, Constant.AppName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }

        /// <summary>
        /// Show thông báo bạn có chắc chắn hủy không
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static DialogResult ShowYesNoQuestion_Cancel(EnumCancelAction action, out string cancelDescription)
        {
            var result = DialogResult.No;
            using (var frm = new FrmCancel())
            {
                frm.CancelAction = action;
                result = frm.ShowDialog();
                cancelDescription = frm.CancelDescription;
            }
            return result;
        }
    }
}
