using Dentist_CorePractice;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Dentist_CorePractice.CsvRecord;


namespace Dentist_CorePractice
{

    public partial class DataMigrationForm : Form
    {
        private List<PatientRecord> allPatientRecords = new List<PatientRecord>();
        private List<TreatmentRecord> allTreatmentRecords = new List<TreatmentRecord>();
        private List<PatientRecord> validPatientRecords = new List<PatientRecord>();
        private List<TreatmentRecord> validTreatmentRecords = new List<TreatmentRecord>();

      
        private Button loadDataButton;
        private Button runValidationButton;
        private Button ingestDataButton;

     
        private DataGridView treatmentGrid;

        private RichTextBox validationLogBox;
        private RichTextBox ingestionLogBox;
        private Label treatmentSummaryLabel;

        public DataMigrationForm()
        {
            InitializeComponent();
        }
        public enum CsvFileType
        {
            Patient,
            Treatment
        }
        private void LoadDataButton_Click(object sender, EventArgs e)
        {
            //updated
                //string patientCsvPath = "patients.csv";
                //string treatmentCsvPath = "treatments.csv";

                // Get file paths from the new text boxes
                string patientCsvPath = patientCsvPathTextBox.Text;
                string treatmentCsvPath = treatmentCsvPathTextBox.Text;

                if (string.IsNullOrWhiteSpace(patientCsvPath) || string.IsNullOrWhiteSpace(treatmentCsvPath))
                {
                    MessageBox.Show("Please specify both Patient CSV and Treatment CSV file paths.", "Missing Files", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                try
                {
                    allPatientRecords = CsvParser.ReadAndValidateCsv<PatientRecord>(patientCsvPath);
                    allTreatmentRecords = CsvParser.ReadAndValidateCsv<TreatmentRecord>(treatmentCsvPath);
                    UpdatePatientGrid(allPatientRecords);
                    UpdateTreatmentGrid(allTreatmentRecords);

                    RunValidationButton.Enabled = true;
                    IngestDataButton.Enabled = false;
                    tabControl1.SelectedTab = tabControl1.TabPages[0];

                    RtbValidationBox.Text = "Data loaded successfully. Click '2. Run Validation' to analyze and filter records.";

                }
                catch(FileNotFoundException ex)
                {
                    MessageBox.Show($"Error loading CSV file: {ex.FileName}. Please ensure the file exists at the specified path.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred during file loading: {ex.Message}", "Loading Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

         


        }

        private void RunValidationButton_Click(object sender, EventArgs e)
        {
            if (allPatientRecords.Count == 0 && allTreatmentRecords.Count == 0)
            {
                MessageBox.Show("Please load CSV files first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            var patientValidationResult = CsvParser.PerformValidation(allPatientRecords);
            var treatmentValidationResult = CsvParser.PerformValidation(allTreatmentRecords);

            validPatientRecords = patientValidationResult.ValidRecords.Cast<PatientRecord>().ToList();
            var invalidPatientRecords = patientValidationResult.InvalidRecords.Cast<PatientRecord>().ToList();

          
            var validPatientCsvIds = new HashSet<string>(validPatientRecords.Select(p => p.CsvId));

            foreach (var record in treatmentValidationResult.AllRecords.Cast<TreatmentRecord>())
            {
                
                if (record.IsValid && !validPatientCsvIds.Contains(record.PatientCsvId))
                {
                    record.IsValid = false;
                    record.Errors.Add("FK Error: Patient ID (PatientCsvId) does not exist in the list of VALID Patient records.");
                }
            }

            validTreatmentRecords = treatmentValidationResult.AllRecords
                .Cast<TreatmentRecord>()
                .Where(t => t.IsValid)
                .ToList();
            var invalidTreatmentRecords = treatmentValidationResult.AllRecords
                .Cast<TreatmentRecord>()
                .Where(t => !t.IsValid)
                .ToList();

           
            UpdatePatientGrid(invalidPatientRecords, validPatientRecords.Count);
            UpdateTreatmentGrid(invalidTreatmentRecords, validTreatmentRecords.Count);

       
            IngestDataButton.Enabled = validPatientRecords.Count > 0 && validTreatmentRecords.Count > 0;
            tabControl1.SelectedTab = tabControl1.TabPages[1]; 
           
            GenerateValidationLog(invalidPatientRecords, invalidTreatmentRecords);

            MessageBox.Show($"Validation complete. Found {invalidPatientRecords.Count} invalid patients and {invalidTreatmentRecords.Count} invalid treatments.", "Validation Status", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void IngestDataButton_Click(object sender, EventArgs e)
        {
            if (validPatientRecords.Count == 0 || validTreatmentRecords.Count == 0)
            {
                MessageBox.Show("No valid data to ingest. Please run validation first.", "Ingestion Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ingestionLogRtBox.Clear();
            tabControl1.SelectedTab = tabControl1.TabPages[1];

            try
            {
                
                string sqlLog = DataIngestor.IngestData(validPatientRecords, validTreatmentRecords);

                ingestionLogRtBox.Text = sqlLog;

                RunValidationButton.Enabled = false;
                IngestDataButton.Enabled = false;

                MessageBox.Show("Data ingestion simulation complete. The Ingestion Log contains the final SQLite INSERT statements.", "Ingestion Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ingestionLogRtBox.Text = $"ERROR DURING INGESTION:\r\n{ex.Message}\r\n{ex.StackTrace}";
                MessageBox.Show("An error occurred during ingestion. Check the log for details.", "Ingestion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            CopySqlButton.Enabled = true;

        }


        private void UpdatePatientGrid(List<PatientRecord> records, int? validCount = null)
        {
            if (patientGrid == null) return;

            patientGrid.DataSource = records;

            foreach (DataGridViewRow row in patientGrid.Rows)
            {
                var record = row.DataBoundItem as PatientRecord;
                if (record != null && !record.IsValid)
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.LightPink;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                }
            }

            if (validCount.HasValue)
            {
                patientSummaryLabel.Text = $"Patient Data: Showing {records.Count} invalid rows. Total valid records: {validCount.Value}.";
            }
            else
            {
                patientSummaryLabel.Text = $"Patient Data: {records.Count} rows loaded. Click 'Run Validation' to analyze.";
            }
        }
        private void UpdateTreatmentGrid(List<TreatmentRecord> records, int? validCount = null)
        {
            if (treatmentGridView == null) return;

            treatmentGridView.DataSource = records;

            foreach (DataGridViewRow row in treatmentGridView.Rows)
            {
                var record = row.DataBoundItem as TreatmentRecord;
                if (record != null && !record.IsValid)
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.LightPink;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                }
            }

            if (validCount.HasValue)
            {
                lblTreatment.Text = $"Treatment Data: Showing {records.Count} invalid rows. Total valid records: {validCount.Value}.";
            }
            else
            {
                lblTreatment.Text = $"Treatment Data: {records.Count} rows loaded. Click 'Run Validation' to analyze.";
            }
        }

        private void GenerateValidationLog(List<PatientRecord> invalidPatients, List<TreatmentRecord> invalidTreatments)
        {
            var log = new System.Text.StringBuilder();

            log.AppendLine("--- Patient Validation Summary (tblPatient) ---");
            log.AppendLine($"Total Patients Loaded: {allPatientRecords.Count}");
            log.AppendLine($"Valid Patients for Ingestion: {validPatientRecords.Count}");
            log.AppendLine($"Invalid Patients Skipped: {invalidPatients.Count}\r\n");

            if (invalidPatients.Any())
            {
                log.AppendLine("--- Patient Validation Details (First 10 Errors) ---");
                foreach (var p in invalidPatients.Take(10))
                {
                    log.AppendLine($"Row {p.RowIndex} (CSV ID: {p.CsvId}): {string.Join("; ", p.Errors)}");
                }
                log.AppendLine(invalidPatients.Count > 10 ? "...\r\n" : "\r\n");
            }


            log.AppendLine("--- Treatment Validation Summary (tblTreatment) ---");
            log.AppendLine($"Total Treatments Loaded: {allTreatmentRecords.Count}");
            log.AppendLine($"Valid Treatments for Ingestion: {validTreatmentRecords.Count}");
            log.AppendLine($"Invalid Treatments Skipped: {invalidTreatments.Count}\r\n");

            if (invalidTreatments.Any())
            {
                log.AppendLine("--- Treatment Validation Details (First 10 Errors) ---");
                foreach (var t in invalidTreatments.Take(10))
                {
                    log.AppendLine($"Row {t.RowIndex} (CSV ID: {t.CsvId}, Patient ID: {t.PatientCsvId}): {string.Join("; ", t.Errors)}");
                }
                log.AppendLine(invalidTreatments.Count > 10 ? "...\r\n" : "\r\n");
            }

            RtbValidationBox.Text = log.ToString();
        }
        private void treatmentReviewTab_Click(object sender, EventArgs e)
        {

        }

        private void patientSummaryLabel_Click(object sender, EventArgs e)
        {

        }

        private void CopySqlButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ingestionLogRtBox.Text))
            {
                MessageBox.Show("Ingestion Log is empty. Please run '3. Ingest Data' first.", "Copy Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Clipboard.SetText(ingestionLogRtBox.Text);
                MessageBox.Show("SQL statements copied to clipboard! You can now paste them into the DB Browser for SQLite 'Execute SQL' tab.", "Copy Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error copying to clipboard: {ex.Message}", "Copy Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void browseTreatmentCsvButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.CurrentDirectory;
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    treatmentCsvPathTextBox.Text = openFileDialog.FileName;
                }
            }
        }

        private void BrowsePatientCsvButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.CurrentDirectory;
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    patientCsvPathTextBox.Text = openFileDialog.FileName;
                }
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void RtbValidationBox_TextChanged(object sender, EventArgs e)
        {

        }
    }



    public enum CsvFileType
    {
        Patient,
        Treatment
    }


    public abstract class CsvRecord
    {
        public int RowIndex { get; set; }
        public string CsvId { get; set; }

        public bool IsValid { get; set; } = true;
        public List<string> Errors { get; set; } = new List<string>();

        public abstract CsvFileType FileType { get; }
        public abstract void Validate();
    }

        public class PatientRecord : CsvRecord
        {
            public override CsvFileType FileType => CsvFileType.Patient;

            public string RawFirstName { get; set; }
            public string RawLastName { get; set; }
            public string RawDOB { get; set; }
            public string RawEmail { get; set; }
            public string RawMobile { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime? DateOfBirth { get; set; }
            public string Email { get; set; }
            public string Mobile { get; set; }

            public string Gender { get; set; }
            public string Street { get; set; }
            public string Postcode { get; set; }


            public override void Validate()
            {
                Errors.Clear();
                // Validation 1: Mandatory Fields
                if (string.IsNullOrWhiteSpace(RawFirstName)) Errors.Add("First Name is mandatory.");
                if (string.IsNullOrWhiteSpace(RawLastName)) Errors.Add("Last Name is mandatory.");

                // Validation 2: Date Format (Strict ISO YYYY-MM-DD)
                if (string.IsNullOrWhiteSpace(RawDOB))
                {
                    Errors.Add("Date of Birth is mandatory.");
                }
                else if (DateTime.TryParseExact(RawDOB, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime dob))
                {
                    DateOfBirth = dob;
                }
                else
                {
                    Errors.Add("Date of Birth format is invalid (required: YYYY-MM-DD).");
                }

                // Validation 3: Email Format
                if (!string.IsNullOrWhiteSpace(RawEmail))
                {
                    // Simple regex for email validation
                    if (!Regex.IsMatch(RawEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
                    {
                        Errors.Add("Email format is invalid.");
                    }
                    else
                    {
                        Email = RawEmail;
                    }
                }

                // Validation 4: Mobile Number (simple presence check, sanitization is complex and omitted)
                if (string.IsNullOrWhiteSpace(RawMobile)) Errors.Add("Mobile Number is mandatory.");

                // Finalize validation status
                if (Errors.Count > 0)
                {
                    IsValid = false;
                }
                else
                {
                    // Assign cleaned/validated values
                    FirstName = RawFirstName;
                    LastName = RawLastName;
                    Mobile = RawMobile;
                    IsValid = true;
                }
            }
        }


        public class TreatmentRecord : CsvRecord
        {
            public override CsvFileType FileType => CsvFileType.Treatment;

          
            public string PatientCsvId { get; set; } 
            public string RawItemCode { get; set; }
            public string RawFee { get; set; }
            public string RawDate { get; set; }

            public decimal Fee { get; set; }
            public DateTime CompleteDate { get; set; }
            public string ItemCode { get; set; }

            public string Description { get; set; }
            public string ToothNumber { get; set; }
            public string Surface { get; set; }

          
            public override void Validate()
            {
                Errors.Clear();

                if (string.IsNullOrWhiteSpace(PatientCsvId)) Errors.Add("Patient ID (FK) is mandatory.");
                if (string.IsNullOrWhiteSpace(RawItemCode)) Errors.Add("Item Code is mandatory.");
                if (string.IsNullOrWhiteSpace(Description)) Errors.Add("Description is mandatory.");

                if (!decimal.TryParse(RawFee, out decimal fee))
                {
                    Errors.Add("Fee is not a valid number.");
                }
                else
                {
                    Fee = fee;
                }

                if (string.IsNullOrWhiteSpace(RawDate))
                {
                    Errors.Add("Date is mandatory.");
                }
                else if (DateTime.TryParseExact(RawDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime date))
                {
                    CompleteDate = date;
                }
                else
                {
                    Errors.Add("Date format is invalid (required: YYYY-MM-DD).");
                }

                if (Errors.Count > 0)
                {
                    IsValid = false;
                }
                else
                {
                    ItemCode = RawItemCode;
                    IsValid = true;
                }
            }
        }

        public static class CsvParser
        {
            private const string DateFormat = "yyyy-MM-dd";

            public static List<T> ReadAndValidateCsv<T>(string filePath) where T : CsvRecord, new()
            {
                var records = new List<T>();
                var lines = File.ReadAllLines(filePath);

                for (int i = 1; i < lines.Length; i++)
                {
                    var parts = lines[i].Split(',');
                    var record = new T { RowIndex = i + 1 };

                    try
                    {

                        if (record.FileType == CsvFileType.Patient && parts.Length >= 12)
                        {
                            var p = (PatientRecord)(object)record;
                            p.CsvId = parts[0].Trim();           // Id
                            p.RawFirstName = parts[1].Trim();      // FirstName
                            p.RawLastName = parts[2].Trim();       // LastName
                            p.RawDOB = parts[3].Trim();            // DOB
                            p.Gender = parts[4].Trim();            // Gender
                            p.RawEmail = parts[5].Trim();          // Email
                            p.RawMobile = parts[6].Trim();         // MobileNumber

                            p.Street = parts[8].Trim();
                            p.Postcode = parts[11].Trim();
                        }
                        else if (record.FileType == CsvFileType.Treatment && parts.Length >= 11)
                        {
                            var t = (TreatmentRecord)(object)record;
                            t.CsvId = parts[0].Trim();             // Id
                            t.PatientCsvId = parts[1].Trim();        // PatientID (FK)
                            t.RawItemCode = parts[3].Trim();         // TreatmentItem
                            t.Description = parts[4].Trim();         // Description
                            t.RawFee = parts[6].Trim();              // Fee (not Price)
                            t.RawDate = parts[7].Trim();             // Date
                            t.ToothNumber = parts[9].Trim();         // ToothNumber
                            t.Surface = parts[10].Trim();            // Surface
                        }


                        record.Validate();
                        records.Add(record);
                    }
                    catch (Exception ex)
                    {
                        record.IsValid = false;
                        record.Errors.Add($"General Parsing Error: {ex.Message}");
                        records.Add(record);
                    }
                }
                return records;
            }


        public static (List<CsvRecord> ValidRecords, List<CsvRecord> InvalidRecords, List<CsvRecord> AllRecords) PerformValidation(IEnumerable<CsvRecord> records)
        {
            var valid = new List<CsvRecord>();
            var invalid = new List<CsvRecord>();
            var all = records.ToList(); 

            foreach (var record in all)
            {
               
                record.Validate();

                if (record.IsValid)
                {
                    valid.Add(record);
                }
                else
                {
                    invalid.Add(record);
                }
            }
            return (valid, invalid, all);
        }
    }

        public static class DataIngestor
        {
            private static int patientIdCounter = 1;
            private static int treatmentIdCounter = 1;
            private static int invoiceIdCounter = 1;
            private static int invoiceLineIdCounter = 1;
            private static int invoiceNoCounter = 1;

            public static string IngestData(List<PatientRecord> validPatients, List<TreatmentRecord> validTreatments)
            {
                var log = new System.Text.StringBuilder();


                patientIdCounter = 1;
                treatmentIdCounter = 1;
                invoiceIdCounter = 1;
                invoiceLineIdCounter = 1;
                invoiceNoCounter = 1;


                var patientIdMap = new Dictionary<string, int>();


                log.AppendLine("--- 1. INGESTING tblPatient RECORDS ---");

                foreach (var p in validPatients)
                {
                    int newPatientId = patientIdCounter++;
                    patientIdMap.Add(p.CsvId, newPatientId);

                    string sql = $@"INSERT INTO tblPatient (PatientId, PatientIdentifier, Firstname, Lastname, DateOfBirth, Email, Mobile, Gender, AddressLine1, Postcode, IsDeleted) 
VALUES ({newPatientId}, {GetSqlValue(p.CsvId)}, {GetSqlValue(p.FirstName)}, {GetSqlValue(p.LastName)}, {GetSqlValue(p.DateOfBirth)}, {GetSqlValue(p.Email)}, {GetSqlValue(p.Mobile)}, {GetSqlValue(p.Gender)}, {GetSqlValue(p.Street)}, {GetSqlValue(p.Postcode)}, 0);";
                    log.AppendLine(sql);
                }


                log.AppendLine("\r\n--- 2. GROUPING TREATMENTS AND CREATING tblInvoice RECORDS ---");


                var treatmentsByInvoiceGroup = validTreatments
                    .GroupBy(t => new { PatientId = t.PatientCsvId, Date = t.CompleteDate.Date })
                    .ToList();

                foreach (var group in treatmentsByInvoiceGroup)
                {

                    if (!patientIdMap.TryGetValue(group.Key.PatientId, out int internalPatientId))
                    {
                        log.AppendLine($"-- SKIPPED Invoice Group: Could not find internal ID for legacy Patient ID: {group.Key.PatientId}");
                        continue;
                    }

                    int newInvoiceId = invoiceIdCounter++;
                    int newInvoiceNo = invoiceNoCounter++;
                    decimal totalAmount = group.Sum(t => t.Fee);


                    string invoiceDateSql = GetSqlValue(group.Key.Date);


                    string invoiceSql = $@"INSERT INTO tblInvoice (InvoiceId, InvoiceIdentifier, InvoiceNo, InvoiceDate, Total, PatientId, IsDeleted) 
VALUES ({newInvoiceId}, {GetSqlValue($"INV-{newInvoiceId}")}, {newInvoiceNo}, {invoiceDateSql}, {totalAmount}, {internalPatientId}, 0);";
                    log.AppendLine(invoiceSql);


                    foreach (var t in group)
                    {
                        int newTreatmentId = treatmentIdCounter++;

                        string treatmentSql = $@"INSERT INTO tblTreatment (TreatmentId, TreatmentIdentifier, CompleteDate, Description, ItemCode, Tooth, Surface, Quantity, Fee, InvoiceId, PatientId, IsPaid, IsVoided) 
VALUES ({newTreatmentId}, {GetSqlValue(t.CsvId)}, {GetSqlValue(t.CompleteDate)}, {GetSqlValue(t.Description)}, {GetSqlValue(t.ItemCode)}, {GetSqlValue(t.ToothNumber)}, {GetSqlValue(t.Surface)}, 1, {t.Fee}, {newInvoiceId}, {internalPatientId}, 0, 0);";
                        log.AppendLine(treatmentSql);

                        int newLineId = invoiceLineIdCounter++;
                        string lineSql = $@"INSERT INTO tblInvoiceLineItem (InvoiceLineItemId, InvoiceLineItemIdentifier, Description, ItemCode, Quantity, UnitAmount, LineAmount, PatientId, TreatmentId, InvoiceId) 
VALUES ({newLineId}, {GetSqlValue($"LIN-{newLineId}")}, {GetSqlValue(t.Description)}, {GetSqlValue(t.ItemCode)}, 1, {t.Fee}, {t.Fee}, {internalPatientId}, {newTreatmentId}, {newInvoiceId});";
                        log.AppendLine(lineSql);
                    }
                }


                log.AppendLine("\r\n--- 4. INGESTION SUMMARY ---");
                log.AppendLine($"Total Patients Inserted (tblPatient): {patientIdCounter - 1}");
                log.AppendLine($"Total Invoices Created (tblInvoice): {invoiceIdCounter - 1}");
                log.AppendLine($"Total Treatments Inserted (tblTreatment): {treatmentIdCounter - 1}");
                log.AppendLine($"Total Line Items Created (tblInvoiceLineItem): {invoiceLineIdCounter - 1}");

                return log.ToString();
            }


            private static string GetSqlValue(object value)
            {

                if (value == null)
                {
                    return "NULL";
                }

                if (value is int || value is decimal || value is float || value is double)
                {

                    return value.ToString().Replace(',', '.');
                }

                if (value is bool booleanValue)
                {
                    return booleanValue ? "1" : "0";
                }

                if (value is DateTime dateTimeValue)
                {
                    return $"'{dateTimeValue.ToString("yyyy-MM-dd HH:mm:ss")}'";
                }


                string stringValue = value.ToString();

                if (string.IsNullOrWhiteSpace(stringValue))
                {
                    return "NULL";
                }


                stringValue = stringValue.Replace("'", "''");
                return $"'{stringValue}'";
            }
        }
    }

