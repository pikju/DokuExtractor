﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DokuExtractorCore.Model;

namespace DokuExtractorStandardGUI.UserControlsTemplateEditor
{
    public partial class ucDataField : UserControl
    {
        public delegate void RegexExpressionHelperHandler(Guid id, DataFieldTypes dataFieldType);
        /// <summary>
        /// Fired, when user wishes to start the regex expression helper
        /// </summary>
        public event RegexExpressionHelperHandler RegexExpressionHelper;

        public string NameText { get { return txtName.Text; } }
        public int FieldTypeInt { get { return lbxFieldType.SelectedIndex; } }
        public string RegexText { get { return txtRegexExpression.Text; } }

        private DataFieldTemplate dataField { get; set; } = new DataFieldTemplate();

        public ucDataField()
        {
            InitializeComponent();
        }

        public ucDataField(DataFieldTemplate dataField)
        {
            InitializeComponent();
            this.dataField = dataField;
        }

        private void ucDataField_Load(object sender, EventArgs e)
        {
            txtName.Text = this.dataField.Name;

            lbxFieldType.SelectedIndex = (int)(this.dataField.FieldType);

            if (dataField.RegexExpressions != null)
                foreach (var item in dataField.RegexExpressions)
                {
                    txtRegexExpression.Text = txtRegexExpression.Text + item + Environment.NewLine;
                }
        }

        public void ActivateRegexExpressionHelper()
        {
            txtRegexExpression.Enabled = false;
            lblRegexExpression.Font = new Font(lblRegexExpression.Font.Name, lblRegexExpression.Font.SizeInPoints, FontStyle.Underline);
            lblRegexExpression.DoubleClick += LblRegexExpression_DoubleClick;
        }

        public void ChangeOrAddRegexExpression(string regex, bool additionalRegex)
        {
            if (additionalRegex)
                txtRegexExpression.Text = txtRegexExpression.Text + regex + Environment.NewLine;
            else
                txtRegexExpression.Text = regex;
        }

        private void LblRegexExpression_DoubleClick(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Start Regex Expression Helper?", "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    var id = (Guid)(this.Tag);
                    FireRegexExpressionHelper(id, (DataFieldTypes)FieldTypeInt);
                }
                catch (Exception ex)
                { }
            }
        }

        private void FireRegexExpressionHelper(Guid id, DataFieldTypes dataFieldType)
        {
            RegexExpressionHelper?.Invoke(id, dataFieldType);
        }
    }
}
