using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterestCalculatorWinForms
{
    public class Form1 : Form
    {
        TextBox txtPrincipal;
        TextBox txtAnnualRate;
        NumericUpDown numDaysInYear;
        NumericUpDown numMonthsInYear;
        Button btnCalc, btnClear, btnExit;
        DataGridView grid;

        public Form1()
        {
            Text = "Interest Calculator";
            MinimumSize = new Size(780, 520);
            StartPosition = FormStartPosition.CenterScreen;

            var lblFont = new Font("Segoe UI", 10f, FontStyle.Regular);
            var inputFont = new Font("Segoe UI", 10f, FontStyle.Regular);

            // Labels
            var lblPrincipal = new Label { Text = "Amount (P):", AutoSize = true, Font = lblFont, Location = new Point(16, 20) };
            var lblRate = new Label { Text = "Annual interest %:", AutoSize = true, Font = lblFont, Location = new Point(16, 60) };
            var lblDays = new Label { Text = "Days in year:", AutoSize = true, Font = lblFont, Location = new Point(16, 100) };
            var lblMonths = new Label { Text = "Months in year:", AutoSize = true, Font = lblFont, Location = new Point(16, 140) };

            // Inputs
            txtPrincipal = new TextBox { Font = inputFont, Location = new Point(180, 16), Width = 180 };
            txtAnnualRate = new TextBox { Font = inputFont, Location = new Point(180, 56), Width = 180 };

            numDaysInYear = new NumericUpDown
            {
                Font = inputFont,
                Location = new Point(180, 96),
                Width = 100,
                Minimum = 360,
                Maximum = 366,
                Value = 365
            };

            numMonthsInYear = new NumericUpDown
            {
                Font = inputFont,
                Location = new Point(180, 136),
                Width = 100,
                Minimum = 1,
                Maximum = 12,
                Value = 12
            };

            // Buttons
            btnCalc = new Button { Text = "Calculate", Font = lblFont, Location = new Point(16, 190), Width = 120, Height = 36 };
            btnClear = new Button { Text = "Clear", Font = lblFont, Location = new Point(150, 190), Width = 120, Height = 36 };
            btnExit = new Button { Text = "Exit", Font = lblFont, Location = new Point(284, 190), Width = 120, Height = 36 };

            btnCalc.Click += (s, e) => CalculateAndFill();
            btnClear.Click += (s, e) => { txtPrincipal.Clear(); txtAnnualRate.Clear(); grid.Rows.Clear(); txtPrincipal.Focus(); };
            btnExit.Click += (s, e) => Close();

            // Results grid
            grid = new DataGridView
            {
                Location = new Point(16, 240),
                Width = 730,
                Height = 220,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                MultiSelect = false,
                RowHeadersVisible = false
            };

            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Period" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Principal (P)" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Amount (Simple Interest)" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Amount (Compound Interest)" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Interest (Compound)" });

            // Tooltips
            var tt = new ToolTip();
            tt.SetToolTip(txtPrincipal, "Enter principal amount, e.g. 1000");
            tt.SetToolTip(txtAnnualRate, "Enter annual interest rate, e.g. 7.5");
            tt.SetToolTip(numDaysInYear, "Usually 360, 365, or 366");
            tt.SetToolTip(numMonthsInYear, "Usually 12");
            tt.SetToolTip(btnCalc, "Calculates for day, month, and year");
            tt.SetToolTip(btnClear, "Clears inputs and results");

            Controls.AddRange(new Control[]
            {
                lblPrincipal, txtPrincipal,
                lblRate, txtAnnualRate,
                lblDays, numDaysInYear,
                lblMonths, numMonthsInYear,
                btnCalc, btnClear, btnExit,
                grid
            });
        }

        private void CalculateAndFill()
        {
            try
            {
                var ci = (CultureInfo)CultureInfo.InvariantCulture.Clone();

                string principalRaw = (txtPrincipal.Text ?? "").Trim().Replace(',', '.');
                string rateRaw = (txtAnnualRate.Text ?? "").Trim().Replace(',', '.');

                decimal P;
                decimal annualRatePercent;

                if (!decimal.TryParse(principalRaw, NumberStyles.Number, ci, out P) || P < 0)
                {
                    MessageBox.Show("Please enter a valid non-negative amount (P).", "Invalid Input",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPrincipal.Focus();
                    return;
                }

                if (!decimal.TryParse(rateRaw, NumberStyles.Number, ci, out annualRatePercent) || annualRatePercent < 0)
                {
                    MessageBox.Show("Please enter a valid non-negative annual rate.", "Invalid Input",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAnnualRate.Focus();
                    return;
                }

                int daysInYear = (int)numDaysInYear.Value;
                int monthsInYear = (int)numMonthsInYear.Value;
                decimal r = annualRatePercent / 100m;

                decimal A_simple_day = SimpleAmount(P, r / daysInYear);
                decimal A_simple_month = SimpleAmount(P, r / monthsInYear);
                decimal A_simple_year = SimpleAmount(P, r);

                decimal A_comp_day = CompoundAmount(P, r, daysInYear, 1);
                decimal A_comp_month = CompoundAmount(P, r, monthsInYear, 1);
                decimal A_comp_year = CompoundAmount(P, r, 1, 1);

                grid.Rows.Clear();
                AddRow("DAY", P, A_simple_day, A_comp_day);
                AddRow("MONTH", P, A_simple_month, A_comp_month);
                AddRow("YEAR", P, A_simple_year, A_comp_year);

                this.Text = $"Interest Calculator • days/year: {daysInYear}, months/year: {monthsInYear}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred during calculation:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static decimal SimpleAmount(decimal principal, decimal periodRate)
        {
            return principal * (1 + periodRate);
        }

        private static decimal CompoundAmount(decimal principal, decimal annualRate, int periodsPerYear, int numberOfPeriods)
        {
            double factor = Math.Pow(1.0 + (double)annualRate / periodsPerYear, numberOfPeriods);
            return principal * (decimal)factor;
        }

        private void AddRow(string periodLabel, decimal P, decimal A_simple, decimal A_comp)
        {
            grid.Rows.Add(
                periodLabel,
                P.ToString("N2"),
                A_simple.ToString("N2"),
                A_comp.ToString("N2"),
                (A_comp - P).ToString("N2")
            );
        }
    }
}
