using System;
using System.IO;
using System.Windows.Forms;
using CsvHelper;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography.X509Certificates;
using static System.Collections.Specialized.BitVector32;
using System.Globalization;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private List<List<string>> data_source;
        private List<List<string>> blank_length_data;
        private List<List<string>> release_order;
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                Title = "Select Data Source File"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filepath = openFileDialog.FileName;
                string directory = Path.GetDirectoryName(filepath);
                string newFileName = "Blank_Length.csv";
                string newFilePath = Path.Combine(directory, newFileName);
                data_source = LoadCsv(filepath);
                blank_length_data = LoadCsv(newFilePath);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                Title = "Select Work Order File"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filepath = openFileDialog.FileName;
                release_order = LoadCsv(filepath);
                DisplayDataInDataGridVIew(release_order);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (data_source == null)
            {
                MessageBox.Show($"Data Source not loaded.");
            }
            if (release_order == null)
            {
                MessageBox.Show($"Release orders not loaded.");
            }
            else
            {
                int n_orders = release_order.Count - 1;
                string[] item_number = new string[n_orders];
                string[] grade_i = new string[n_orders];
                string[] grade_o = new string[n_orders];
                string[] product = new string[n_orders];
                int[] k = new int[n_orders];
                double[] qty = new double[n_orders];
                double[] diameter = new double[n_orders];
                double[] diameter_second = new double[n_orders];
                double[] L_i = new double[n_orders];
                double[] L_o = new double[n_orders];
                double[] L_i_second = new double[n_orders];
                double[] L_o_second = new double[n_orders];
                double[] t_i = new double[n_orders];
                double[] t_o = new double[n_orders];
                double[] plus_value = new double[n_orders];
                for (int i = 1; i <= n_orders; i++)
                {
                    item_number[i - 1] = release_order[i][2];
                    int ind = data_source.FindIndex(row => row[0].ToString().Contains(item_number[i - 1]));
                    k[i - 1] = ind;
                    qty[i - 1] = Convert.ToDouble(release_order[i][4]);
                    if (k[i - 1] == -1)
                    {
                        diameter[i - 1] = 0;
                        diameter_second[i - 1] = 0;
                        L_i[i - 1] = 0;
                        L_o[i - 1] = 0;
                        L_i_second[i - 1] = 0;
                        L_o_second[i - 1] = 0;
                        t_i[i - 1] = 0;
                        t_o[i - 1] = 0;
                        grade_i[i - 1] = "N/A";
                        grade_o[i - 1] = "N/A";
                        product[i - 1] = "N/A";
                        plus_value[i - 1] = 0;
                    }
                    else
                    {
                        diameter[i - 1] = Convert.ToDouble(data_source[ind][3]);
                        diameter_second[i - 1] = Convert.ToDouble(data_source[ind][6]);
                        L_i[i - 1] = Convert.ToDouble(data_source[ind][7]);
                        L_o[i - 1] = Convert.ToDouble(data_source[ind][8]);
                        L_i_second[i - 1] = Convert.ToDouble(data_source[ind][9]);
                        L_o_second[i - 1] = Convert.ToDouble(data_source[ind][10]);
                        t_i[i - 1] = Convert.ToDouble(data_source[ind][11]);
                        t_o[i - 1] = Convert.ToDouble(data_source[ind][12]);
                        product[i - 1] = Convert.ToString(data_source[ind][2]);
                        grade_i[i - 1] = Convert.ToString(data_source[ind][13]);
                        grade_o[i - 1] = Convert.ToString(data_source[ind][14]);
                        plus_value[i - 1] = Convert.ToDouble(data_source[ind][5]);
                    }
                }
                Dictionary<string, int> stringToIntMap = new Dictionary<string, int>();
                Dictionary<double, string> IntToStringMap = new Dictionary<double, string>();
                List<int> assignedIntegers = new List<int>();
                int currentInteger = 1;
                foreach (var item in product)
                {
                    if (!stringToIntMap.ContainsKey(item))
                    {
                        stringToIntMap[item] = currentInteger;
                        IntToStringMap[currentInteger] = item;
                        currentInteger++;
                    }
                    assignedIntegers.Add(stringToIntMap[item]);
                }
                Dictionary<string, int> Gi_stringToIntMap = new Dictionary<string, int>();
                Dictionary<double, string> Gi_IntToStringMap = new Dictionary<double, string>();
                List<int> Gi_assignedIntegers = new List<int>();
                int Gi_currentInteger = 1;
                foreach (var item in grade_i)
                {
                    if (!Gi_stringToIntMap.ContainsKey(item))
                    {
                        Gi_stringToIntMap[item] = Gi_currentInteger;
                        Gi_IntToStringMap[Gi_currentInteger] = item;
                        Gi_currentInteger++;
                    }
                    Gi_assignedIntegers.Add(Gi_stringToIntMap[item]);
                }
                Dictionary<string, int> Go_stringToIntMap = new Dictionary<string, int>();
                Dictionary<double, string> Go_IntToStringMap = new Dictionary<double, string>();
                List<int> Go_assignedIntegers = new List<int>();
                int Go_currentInteger = 1;
                foreach (var item in grade_o)
                {
                    if (!Go_stringToIntMap.ContainsKey(item))
                    {
                        Go_stringToIntMap[item] = Go_currentInteger;
                        Go_IntToStringMap[Go_currentInteger] = item;
                        Go_currentInteger++;
                    }
                    Go_assignedIntegers.Add(Go_stringToIntMap[item]);
                }
                double[,] data_out = new double[n_orders, 13];
                for (int i = 0; i < n_orders; i++)
                {
                    data_out[i, 0] = diameter[i];
                    data_out[i, 1] = diameter_second[i];
                    data_out[i, 2] = L_i[i];
                    data_out[i, 3] = L_o[i];
                    data_out[i, 4] = L_i_second[i];
                    data_out[i, 5] = L_o_second[i];
                    data_out[i, 6] = t_i[i];
                    data_out[i, 7] = t_o[i];
                    data_out[i, 8] = qty[i];
                    data_out[i, 9] = assignedIntegers[i];
                    data_out[i, 10] = Gi_assignedIntegers[i];
                    data_out[i, 11] = Go_assignedIntegers[i];
                    data_out[i, 12] = plus_value[i];
                }
                data_out = SortArrayByColumn(data_out, 9);
                double[] diameter_sort = Enumerable.Range(0, data_out.GetLength(0))
                                 .Select(row => data_out[row, 0])
                                 .ToArray();
                double[] id_sort = Enumerable.Range(0, data_out.GetLength(0))
                                 .Select(row => data_out[row, 9])
                                 .ToArray();
                var product_dia = diameter_sort.Zip(id_sort, (d, i) => d * i).ToArray();
                var uniqueProduc = product_dia.Distinct().ToList();
                int[] idx = new int[n_orders];
                int n = 0;
                double product_id = product_dia[0];
                List <double> ds = new List<double>{ diameter_sort[0] };
                List <double> up = new List<double> { product_dia[0] };    
                for (int i = 0; i < n_orders; i++)
                {
                    if (product_id == product_dia[i])
                    {
                        idx[i] = n;
                        product_id = product_dia[i];
                    }
                    else
                    {
                        n = n + 1;
                        idx[i] = n;
                        product_id = product_dia[i];
                        ds.Add(diameter_sort[i]);
                        up.Add(product_dia[i]);
                    }
                }
                var uniqueIdx = idx.Distinct().ToList();
                double[] dia_set= ds.ToArray();
                double[] product_dia_set= up.ToArray();
                double[] diameter_second_sort = Enumerable.Range(0, data_out.GetLength(0))
                                 .Select(row => data_out[row, 1])
                                 .ToArray();
                double[] second_pipe = diameter_second_sort.Where(d => d != 0).ToArray();
                var product_sdia = diameter_second_sort.Zip(id_sort, (d, i) => d * i).ToArray();
                double[] product_sdia_nonzero = product_sdia.Where(d => d != 0).ToArray();
                var sid = product_sdia_nonzero.Zip(second_pipe, (p, d) => p / d).Select(x => (int)x).ToArray();
                var uniqueDia_s = diameter_second_sort.Distinct().Where(d => d > 0).ToList();
                var unique_product = product_sdia_nonzero.Distinct().Where(d => d > 0).ToList();
                List<int> i_s = new List<int>();
                for (int i = 0; i < diameter_second.Length; i++)
                {
                    if (diameter_second[i] != 0)
                    {
                        i_s.Add(i);
                    }
                }
                int[] i_ss = i_s.ToArray();
                int[] idx_s = new int[uniqueDia_s.Count];
                int n_s = 0;
                double sproduct_id = product_sdia[0];
                for (int i = 0; i < second_pipe.Length; i++)
                {
                    if (sproduct_id == product_sdia[i])
                    {
                        idx_s[i] = n_s;
                        sproduct_id = product_sdia[i];
                    }
                    else
                    {
                        n_s = n_s + 1;
                        idx_s[i] = n_s;
                        sproduct_id = product_sdia[i];
                    }
                }
                var uniqueIdx_s = idx_s.Distinct().ToList();
                double[] unique_th = new double[uniqueIdx.Count];
                double[] total_pipe_i = new double[uniqueIdx.Count];
                double[] total_pipe_o = new double[uniqueIdx.Count];
                double[] total_pipe_s_i = new double[uniqueDia_s.Count];
                double[] total_pipe_s_o = new double[uniqueDia_s.Count];
                double[] unique_th_s = new double[uniqueDia_s.Count];
                double[] int_gradei = new double[uniqueIdx.Count];
                double[] int_gradeo = new double[uniqueIdx.Count];
                double[] int_plus_value = new double[uniqueIdx.Count];
                int counter = 0;
                int idx_count = 0;
                double[] dia_final = new double[uniqueIdx.Count];
                for (int i = 0; i < uniqueIdx.Count; i++)
                {
                    double sum_length_i = 0;
                    double sum_length_o = 0;
                    double sum_th = 0;
                    int counter_th = 0;
                    double gi_id = 0; double go_id = 0;
                    double plus_value_id = 0;
                    for (int j = 0; j < idx.Length; j++)
                    {
                        if (idx[j] == counter)
                        {
                            dia_final[i] = data_out[idx_count, 0];
                            gi_id = data_out[idx_count, 10];
                            go_id = data_out[idx_count, 11];
                            plus_value_id = data_out[idx_count, 12];
                            double sl_i = data_out[idx_count, 8] / Math.Floor(1 / data_out[idx_count, 2]);
                            double sl_o = data_out[idx_count, 8] / Math.Floor(1 / data_out[idx_count, 3]);
                            sum_th += data_out[idx_count, 6];
                            idx_count = idx_count + 1;
                            counter_th = counter_th + 1;
                            sum_length_i = sum_length_i + sl_i;
                            sum_length_o = sum_length_o + sl_o;
                        }
                    }
                    total_pipe_i[i] = Math.Ceiling(sum_length_i);
                    total_pipe_o[i] = Math.Ceiling(sum_length_o);
                    unique_th[i] = sum_th / counter_th;
                    counter = counter + 1;
                    int_gradei[i] = gi_id; int_gradeo[i] = go_id;
                    int_plus_value[i] = plus_value_id;
                }
                for (int i = 0; i < uniqueDia_s.Count; i++)
                {
                    double sum_length_s_i = 0;
                    double sum_length_s_o = 0;
                    for (int j = 0; j < second_pipe.Length; j++)
                    {
                        if (uniqueDia_s[i] == second_pipe[j])
                        {
                            double sls_i = data_out[i_ss[j], 8] / Math.Floor(1 / data_out[i_ss[j], 4]);
                            double sls_o = data_out[i_ss[j], 8] / Math.Floor(1 / data_out[i_ss[j], 5]);
                            sum_length_s_i = sum_length_s_i + sls_i;
                            sum_length_s_o = sum_length_s_o + sls_o;
                        }
                    }
                    total_pipe_s_i[i] = Math.Ceiling(sum_length_s_i);
                    total_pipe_s_o[i] = Math.Ceiling(sum_length_s_o);
                }
                var data_report1 = new DataTable();
                data_report1.Columns.Add("Product", typeof(string));
                data_report1.Columns.Add("Main/Second", typeof(string));
                data_report1.Columns.Add("Diameter", typeof(double));
                data_report1.Columns.Add("Inner/Outer", typeof(string));
                data_report1.Columns.Add("Thickness", typeof(double));
                data_report1.Columns.Add("Pipe Lengths Required (48)", typeof(double));
                data_report1.Columns.Add("Grade", typeof(string));
                data_report1.Columns.Add("Blank Length", typeof(string));
                for (int i = 0; i < uniqueIdx.Count; i++)
                {
                    double id_n = product_dia_set[i] / dia_set[i];
                    string result_i; string result_o;
                    if (IntToStringMap.TryGetValue(id_n, out string originalString))
                    {
                        string p = originalString;
                        var blank_inner = blank_length_data.FirstOrDefault(row =>
                        row[0] == p &&
                        double.TryParse(row[1], out double value2) && value2 == int_plus_value[i] &&
                        double.TryParse(row[2], out double value3) && value3 == dia_final[i] &&
                        double.TryParse(row[3], out double value4) && value4 == unique_th[i] &&
                        row[5] == "I");
                        if (blank_inner != null)
                        {
                            result_i = blank_inner[7];
                        }
                        else
                        {
                            result_i = "-";
                        }
                        var blank_outer = blank_length_data.FirstOrDefault(row =>
                        row[0] == p &&
                        double.TryParse(row[1], out double value2) && value2 == int_plus_value[i] &&
                        double.TryParse(row[2], out double value3) && value3 == dia_final[i] &&
                        double.TryParse(row[3], out double value4) && value4 == unique_th[i] &&
                        row[5] == "O");
                        if (blank_outer != null)
                        {
                            result_o = blank_outer[7];
                        }
                        else
                        {
                            result_o = "-";
                        }
                        if (Gi_IntToStringMap.TryGetValue(int_gradei[i], out string originalString2))
                        {
                            string gi = originalString2;
                            data_report1.Rows.Add(p, "main", dia_final[i], "inner", unique_th[i], total_pipe_i[i], gi, result_i);
                        }
                        if (Go_IntToStringMap.TryGetValue(int_gradeo[i], out string originalString3))
                        {
                            string go = originalString3;
                            data_report1.Rows.Add(p, "main", dia_final[i], "outer", unique_th[i], total_pipe_o[i], go, result_o);
                        }
                    }
                }
                for (int i = 0; i < uniqueIdx_s.Count; i++)
                {
                    if (sid.Length > uniqueDia_s.Count)
                    {
                        double sd_dia = unique_product[i] / sid[i];
                        if (IntToStringMap.TryGetValue(sid[i], out string originalString))
                        {
                            string p = originalString;
                            data_report1.Rows.Add(p, "second", sd_dia, "inner", 0, total_pipe_s_i[i], "-", "-");
                            data_report1.Rows.Add(p, "second", sd_dia, "outer", 0, total_pipe_s_o[i], "-", "-");
                        }
                        else
                        {
                            string p = "-";
                            data_report1.Rows.Add(p, "second", uniqueDia_s[i], "inner", 0, total_pipe_s_i[i], "-", "-");
                            data_report1.Rows.Add(p, "second", uniqueDia_s[i], "outer", 0, total_pipe_s_o[i], "-", "-");
                        }
                    }
                    else
                    {
                        double id = unique_product[i] / uniqueDia_s[i];
                        if (IntToStringMap.TryGetValue(id, out string originalString))
                        {
                            string p = originalString;
                            data_report1.Rows.Add(p, "second", uniqueDia_s[i], "inner", 0, total_pipe_s_i[i], "-", "-");
                            data_report1.Rows.Add(p, "second", uniqueDia_s[i], "outer", 0, total_pipe_s_o[i], "-", "-");
                        }
                        else
                        {
                            string p = "-";
                            data_report1.Rows.Add(p, "second", uniqueDia_s[i], "inner", 0, total_pipe_s_i[i], "-", "-");
                            data_report1.Rows.Add(p, "second", uniqueDia_s[i], "outer", 0, total_pipe_s_o[i], "-", "-");
                        }
                    }

                }
                dataGridView3.DataSource = data_report1;
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV Files (*.csv)|*.csv|Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                    Title = "Save Report 1",
                    DefaultExt = "csv",
                    FileName = "Untitled.csv"
                };
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    MessageBox.Show("File will be saved at: " + filePath);
                    ExportToCsv(data_report1, filePath);
                }
                else
                {
                    MessageBox.Show("Save operation was cancelled.");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (data_source == null)
            {
                MessageBox.Show($"Data Source not loaded.");
            }
            if (release_order == null)
            {
                MessageBox.Show($"Release orders not loaded.");
            }
            else
            {
                int n_orders = release_order.Count - 1;
                string[] item_number = new string[n_orders];
                int[] k = new int[n_orders];
                double[] qty = new double[n_orders];
                string[] product = new string[n_orders];
                double[] diameter = new double[n_orders];
                double[] diameter_second = new double[n_orders];
                double[] L_i = new double[n_orders];
                double[] L_o = new double[n_orders];
                double[] L_i_second = new double[n_orders];
                double[] L_o_second = new double[n_orders];
                double[] t_i = new double[n_orders];
                double[] t_o = new double[n_orders];
                for (int i = 1; i <= n_orders; i++)
                {
                    item_number[i - 1] = release_order[i][2];
                    int ind = data_source.FindIndex(row => row[0].ToString().Contains(item_number[i - 1]));
                    k[i - 1] = ind;
                    qty[i - 1] = Convert.ToDouble(release_order[i][4]);
                    if (k[i - 1] == -1)
                    {
                        diameter[i - 1] = 0;
                        diameter_second[i - 1] = 0;
                        L_i[i - 1] = 0;
                        L_o[i - 1] = 0;
                        L_i_second[i - 1] = 0;
                        L_o_second[i - 1] = 0;
                        t_i[i - 1] = 0;
                        t_o[i - 1] = 0;
                        product[i - 1] = "N/A";
                    }
                    else
                    {
                        diameter[i - 1] = Convert.ToDouble(data_source[ind][3]);
                        diameter_second[i - 1] = Convert.ToDouble(data_source[ind][6]);
                        L_i[i - 1] = Convert.ToDouble(data_source[ind][7]);
                        L_o[i - 1] = Convert.ToDouble(data_source[ind][8]);
                        L_i_second[i - 1] = Convert.ToDouble(data_source[ind][9]);
                        L_o_second[i - 1] = Convert.ToDouble(data_source[ind][10]);
                        t_i[i - 1] = Convert.ToDouble(data_source[ind][11]);
                        t_o[i - 1] = Convert.ToDouble(data_source[ind][12]);
                        product[i - 1] = Convert.ToString(data_source[ind][2]);
                    }
                }
                Dictionary<string, int> stringToIntMap = new Dictionary<string, int>();
                Dictionary<double, string> IntToStringMap = new Dictionary<double, string>();
                List<int> assignedIntegers = new List<int>();
                int currentInteger = 1;
                foreach (var item in product)
                {
                    if (!stringToIntMap.ContainsKey(item))
                    {
                        stringToIntMap[item] = currentInteger;
                        IntToStringMap[currentInteger] = item;
                        currentInteger++;
                    }
                    assignedIntegers.Add(stringToIntMap[item]);
                }
                double[,] data_out = new double[n_orders, 10];
                for (int i = 0; i < n_orders; i++)
                {
                    data_out[i, 0] = diameter[i];
                    data_out[i, 1] = diameter_second[i];
                    data_out[i, 2] = L_i[i];
                    data_out[i, 3] = L_o[i];
                    data_out[i, 4] = L_i_second[i];
                    data_out[i, 5] = L_o_second[i];
                    data_out[i, 6] = t_i[i];
                    data_out[i, 7] = t_o[i];
                    data_out[i, 8] = qty[i];
                    data_out[i, 9] = assignedIntegers[i];
                }
                var data_report2 = new DataTable();
                data_report2.Columns.Add("Diameter", typeof(double));                   //data_out[i,0]
                data_report2.Columns.Add("inner/outer", typeof(string));                //"inner/outer"
                data_report2.Columns.Add("Main/second", typeof(string));                //"main/second"
                data_report2.Columns.Add("WO", typeof(string));                         //release_order[i+1][0]
                data_report2.Columns.Add("Description", typeof(string));                //release_order[i+1][3]
                data_report2.Columns.Add("Product ID", typeof(int));
                data_report2.Columns.Add("Length of 48 Pipe", typeof(double));          //data_out[i,2] , data_out[i,3]
                data_report2.Columns.Add("WO Qty", typeof(int));                        //data_out[i,8]
                data_report2.Columns.Add("Total number of sections", typeof(double));
                data_report2.Columns.Add("Remainder from each length", typeof(double));
                //data_report2.Columns.Add("Keep", typeof(string));                
                for (int i = 0; i < n_orders; i++)
                {
                    if (data_out[i, 0] == 0)
                    {
                        string in_out = "Not found in Data Source";
                        double total_sections_i = 0;
                        double remainder_i = 0;
                        data_report2.Rows.Add(data_out[i, 0], in_out, "N/A", release_order[i + 1][0], release_order[i + 1][3], data_out[i, 9], data_out[i, 2], data_out[i, 8], total_sections_i, remainder_i);
                    }
                    else
                    {
                        string in_out = "inner";
                        if (data_out[i, 2] == 0)
                        {
                            double total_sections_i = 0;
                            double remainder_i = 0;
                            double pipe_rem_i = remainder_i * data_out[i, 8];
                            data_report2.Rows.Add(data_out[i, 0], in_out, "Main", release_order[i + 1][0], release_order[i + 1][3], data_out[i, 9], data_out[i, 2], data_out[i, 8], total_sections_i, remainder_i);
                            if (data_out[i, 3] > 0)
                            {
                                in_out = "outer";
                                double total_sections_o = data_out[i, 8] / Math.Floor(1 / data_out[i, 3]);
                                double remainder_o = Math.Round((1 % data_out[i, 3]), 2);
                                //double remainder_o = total_sections_o - (data_out[i, 8] * data_out[i, 3]);
                                double pipe_rem_o = remainder_o * data_out[i, 8];
                                data_report2.Rows.Add(data_out[i, 0], in_out, "Main", release_order[i + 1][0], release_order[i + 1][3], data_out[i, 9], data_out[i, 3], data_out[i, 8], total_sections_i, remainder_o);
                            }
                            if (data_out[i, 2] > 0)
                            {

                            }
                        }
                        else
                        {
                            double total_sections_i = data_out[i, 8] / Math.Floor(1 / data_out[i, 2]);
                            double remainder_i = Math.Round((1 % data_out[i, 2]), 2);
                            //double remainder_i = total_sections_i - (data_out[i, 8] * data_out[i, 2]);
                            double pipe_rem_i = remainder_i * data_out[i, 8];
                            data_report2.Rows.Add(data_out[i, 0], in_out, "main", release_order[i + 1][0], release_order[i + 1][3], data_out[i, 9], data_out[i, 2], data_out[i, 8], total_sections_i, remainder_i);
                            if (data_out[i, 3] > 0)
                            {
                                in_out = "outer";
                                double total_sections_o = data_out[i, 8] / Math.Floor(1 / data_out[i, 3]);
                                double remainder_o = Math.Round((1 % data_out[i, 3]), 2);
                                //double remainder_o = total_sections_o - (data_out[i, 8] * data_out[i, 3]);
                                double pipe_rem_o = remainder_o * data_out[i, 8];
                                data_report2.Rows.Add(data_out[i, 0], in_out, "main", release_order[i + 1][0], release_order[i + 1][3], data_out[i, 9], data_out[i, 3], data_out[i, 8], total_sections_i, remainder_o);
                            }
                            if (data_out[i, 1] > 0)
                            {
                                in_out = "inner";
                                double total_sections_si = data_out[i, 8] / Math.Floor(1 / data_out[i, 4]);
                                double remainder_si = Math.Round((1 % data_out[i, 4]), 2);
                                //double remainder_si = total_sections_si - (data_out[i, 8] * data_out[i, 4]);
                                data_report2.Rows.Add(data_out[i, 1], in_out, "second", release_order[i + 1][0], release_order[i + 1][3], data_out[i, 9], data_out[i, 5], data_out[i, 8], total_sections_si, remainder_si);
                                if (data_out[i, 5] > 0)
                                {
                                    in_out = "outer";
                                    double total_sections_so = data_out[i, 8] / Math.Floor(1 / data_out[i, 5]);
                                    double remainder_so = Math.Round((1 % data_out[i, 5]), 2);
                                    //double remainder_so = total_sections_so - (data_out[i, 8] * data_out[i, 5]);
                                    data_report2.Rows.Add(data_out[i, 1], in_out, "second", release_order[i + 1][0], release_order[i + 1][3], data_out[i, 9], data_out[i, 5], data_out[i, 8], total_sections_so, remainder_so);
                                }
                            }
                        }
                    }

                }
                var categories = data_report2.AsEnumerable()
                                            .Select(row => row.Field<string>("inner/outer"))
                                            .Distinct()
                                            .ToList();
                DataTable data_report_inner = data_report2.Clone();
                DataTable data_report_outer = data_report2.Clone();
                foreach (DataRow row in data_report2.Rows)
                {
                    if (row["inner/outer"].ToString() == categories[0])
                        data_report_inner.ImportRow(row);
                    else
                        data_report_outer.ImportRow(row);
                }
                data_report_inner = SortDataTableDescending(data_report_inner, "Length of 48 Pipe");
                data_report_outer = SortDataTableDescending(data_report_outer, "Length of 48 Pipe");
                int n = 1; int nn = 0;
                int[] m = new int[data_report_inner.Rows.Count];
                data_report_inner.Columns.Add("48' Pipe Tag", typeof(string));
                data_report_inner.Columns.Add("Keep Scrap", typeof(string));
                List<double[]> remainder = new List<double[]>();
                List<int[]> rem_tag = new List<int[]>();
                double rem = 0;
                for (int i = 0; i < data_report_inner.Rows.Count; i++)
                {
                    if (m[i] == 0)
                    {
                        n = n + nn;                       
                        if (Convert.ToDouble(data_report_inner.Rows[i]["Remainder from each length"]) > 0)
                        {
                            if (i < (data_report_inner.Rows.Count - 1))
                            {
                                for (int j = i + 1; j < data_report_inner.Rows.Count; j++)
                                {
                                    if (Convert.ToDouble(data_report_inner.Rows[i]["Diameter"]) == Convert.ToDouble(data_report_inner.Rows[j]["Diameter"])
                                        && Convert.ToDouble(data_report_inner.Rows[i]["Product ID"]) == Convert.ToDouble(data_report_inner.Rows[j]["Product ID"]))
                                    {
                                        if (Convert.ToDouble(data_report_inner.Rows[i]["Remainder from each length"])
                                            >= Convert.ToDouble(data_report_inner.Rows[j]["Length of 48 Pipe"]))
                                        {
                                            nn = Convert.ToInt32(data_report_inner.Rows[i]["Total number of sections"]);
                                            int[] Pipe_tag = Enumerable.Range(n, nn).ToArray();
                                            string Pipe_tag_s = string.Join(",", Pipe_tag);
                                            data_report_inner.Rows[i]["48' Pipe Tag"] = Pipe_tag_s;
                                            data_report_inner.Rows[i]["Keep Scrap"] = "Yes";
                                            double pipe_diff = (Convert.ToDouble(data_report_inner.Rows[i]["Remainder from each length"]) * Convert.ToInt32(data_report_inner.Rows[i]["WO Qty"]))
                                                - (Convert.ToDouble(data_report_inner.Rows[j]["Length of 48 Pipe"]) * Convert.ToInt32(data_report_inner.Rows[j]["WO Qty"]));
                                            double xx = pipe_diff / Convert.ToDouble(data_report_inner.Rows[j]["Length of 48 Pipe"]);
                                            int x = (int)Math.Ceiling(xx);
                                            int[] Pipe_tag_u = Pipe_tag;
                                            if (x > 0)
                                            {
                                                Pipe_tag_u = Pipe_tag[..x];
                                                rem = Convert.ToDouble(data_report_inner.Rows[i]["Remainder from each length"]) * (Pipe_tag.Length - Pipe_tag_u.Length);
                                            }
                                            if (x < 0)
                                            {
                                                int n_1 = n + nn;
                                                int[] Pipe_tag_uu = Enumerable.Range(n_1, Math.Abs(x)).ToArray();
                                                Pipe_tag_u = Pipe_tag_u.Concat(Pipe_tag_uu).ToArray();
                                                rem = (1 - Convert.ToDouble(data_report_inner.Rows[j]["Length of 48 Pipe"])) * (Pipe_tag_uu.Length);
                                            }
                                            string Pipe_tag_ss = string.Join(",", Pipe_tag_u);
                                            data_report_inner.Rows[j]["48' Pipe Tag"] = Pipe_tag_ss;
                                            data_report_inner.Rows[j]["Keep Scrap"] = "No";
                                            n = n + Math.Abs(x);
                                            rem_tag.Add(Pipe_tag_u.Skip(Math.Max(0, Pipe_tag_u.Length - Math.Abs(x))).ToArray());
                                            double[] r = {Convert.ToDouble(data_report_inner.Rows[i]["Diameter"]), Convert.ToDouble(data_report_inner.Rows[i]["Product ID"]),
                                               Math.Abs(x),rem};
                                            remainder.Add(r);
                                            m[j] = 1;
                                            break;
                                        }
                                        else
                                        {
                                            nn = Convert.ToInt32(data_report_inner.Rows[i]["Total number of sections"]);
                                            int[] Pipe_tag = Enumerable.Range(n, nn).ToArray();
                                            string Pipe_tag_s = string.Join(",", Pipe_tag);
                                            data_report_inner.Rows[i]["48' Pipe Tag"] = Pipe_tag_s;
                                            data_report_inner.Rows[i]["Keep Scrap"] = "No";
                                        }
                                    }
                                    else
                                    {
                                        nn = Convert.ToInt32(data_report_inner.Rows[i]["Total number of sections"]);
                                        int[] Pipe_tag = Enumerable.Range(n, nn).ToArray();
                                        string Pipe_tag_s = string.Join(",", Pipe_tag);
                                        data_report_inner.Rows[i]["48' Pipe Tag"] = Pipe_tag_s;
                                        data_report_inner.Rows[i]["Keep Scrap"] = "No";
                                    }
                                }
                            }
                            else
                            {
                                nn = Convert.ToInt32(data_report_inner.Rows[i]["Total number of sections"]);
                                int[] Pipe_tag = Enumerable.Range(n, nn).ToArray();
                                string Pipe_tag_s = string.Join(",", Pipe_tag);
                                data_report_inner.Rows[i]["48' Pipe Tag"] = Pipe_tag_s;
                                data_report_inner.Rows[i]["Keep Scrap"] = "No Scrap";
                            }
                        }
                        else
                        {
                            nn = Convert.ToInt32(data_report_inner.Rows[i]["Total number of sections"]);
                            int[] Pipe_tag = Enumerable.Range(n, nn).ToArray();
                            string Pipe_tag_s = string.Join(",", Pipe_tag);
                            data_report_inner.Rows[i]["48' Pipe Tag"] = Pipe_tag_s;
                            data_report_inner.Rows[i]["Keep Scrap"] = "No Scrap";
                        }
                    }
                }
                int n_o = 1; int nn_o = 0;
                int[] m_o = new int[data_report_outer.Rows.Count];
                data_report_outer.Columns.Add("48' Pipe Tag", typeof(string));
                data_report_outer.Columns.Add("Keep Scrap", typeof(string));
                List<double[]> remainder_out = new List<double[]>();
                List<int[]> rem_tag_o = new List<int[]>();
                double rem_o = 0;
                for (int s = 0; s < data_report_outer.Rows.Count; s++)
                {
                    if (m_o[s] == 0)
                    {
                        n_o = n_o + nn_o;
                        if (Convert.ToDouble(data_report_outer.Rows[s]["Remainder from each length"]) > 0)
                        {
                            if (s < (data_report_outer.Rows.Count - 1))
                            {
                                for (int d = s + 1; d < data_report_outer.Rows.Count; d++)
                                {
                                    if (Convert.ToDouble(data_report_outer.Rows[s]["Diameter"]) == Convert.ToDouble(data_report_outer.Rows[d]["Diameter"])
                                        && Convert.ToDouble(data_report_outer.Rows[s]["Product ID"]) == Convert.ToDouble(data_report_outer.Rows[d]["Product ID"]))
                                    {
                                        if (Convert.ToDouble(data_report_outer.Rows[s]["Remainder from each length"])
                                            >= Convert.ToDouble(data_report_outer.Rows[d]["Length of 48 Pipe"]))
                                        {
                                            nn_o = Convert.ToInt32(data_report_outer.Rows[s]["Total number of sections"]);
                                            int[] Pipe_tag = Enumerable.Range(n_o, nn_o).ToArray();
                                            string Pipe_tag_s = string.Join(",", Pipe_tag);
                                            data_report_outer.Rows[s]["48' Pipe Tag"] = Pipe_tag_s;
                                            data_report_outer.Rows[s]["Keep Scrap"] = "Yes";
                                            double pipe_diff = (Convert.ToDouble(data_report_outer.Rows[s]["Remainder from each length"]) * Convert.ToInt32(data_report_outer.Rows[s]["WO Qty"]))
                                                - (Convert.ToDouble(data_report_outer.Rows[d]["Length of 48 Pipe"]) * Convert.ToInt32(data_report_outer.Rows[d]["WO Qty"]));
                                            double xx = pipe_diff / Convert.ToDouble(data_report_outer.Rows[d]["Length of 48 Pipe"]);
                                            int x = (int)Math.Floor(xx);
                                            int[] Pipe_tag_u = Pipe_tag;
                                            if (x > 0)
                                            {
                                                Pipe_tag_u = Pipe_tag[..x];
                                                rem_o = Convert.ToDouble(data_report_outer.Rows[s]["Remainder from each length"]) * (Pipe_tag.Length - Pipe_tag_u.Length);
                                            }
                                            if (x < 0)
                                            {
                                                int n_1 = n_o + nn_o;
                                                int[] Pipe_tag_uu = Enumerable.Range(n_1, Math.Abs(x)).ToArray();
                                                Pipe_tag_u = Pipe_tag_u.Concat(Pipe_tag_uu).ToArray();
                                                rem_o = (1 - Convert.ToDouble(data_report_outer.Rows[d]["Length of 48 Pipe"])) * (Pipe_tag_uu.Length);
                                            }
                                            string Pipe_tag_ss = string.Join(",", Pipe_tag_u);
                                            data_report_outer.Rows[d]["48' Pipe Tag"] = Pipe_tag_ss;
                                            data_report_outer.Rows[d]["Keep Scrap"] = "No";
                                            n_o = n_o + Math.Abs(x);
                                            rem_tag_o.Add(Pipe_tag_u.Skip(Math.Max(0, Pipe_tag_u.Length - Math.Abs(x))).ToArray());
                                            double[] r = {Convert.ToDouble(data_report_outer.Rows[s]["Diameter"]), Convert.ToDouble(data_report_outer.Rows[s]["Product ID"]),
                                            Math.Abs(x),rem_o};
                                            remainder_out.Add(r);
                                            m_o[d] = 1;
                                            break;
                                        }
                                        else
                                        {
                                            nn_o = Convert.ToInt32(data_report_outer.Rows[s]["Total number of sections"]);
                                            int[] Pipe_tag = Enumerable.Range(n_o, nn_o).ToArray();
                                            string Pipe_tag_s = string.Join(",", Pipe_tag);
                                            data_report_outer.Rows[s]["48' Pipe Tag"] = Pipe_tag_s;
                                            data_report_outer.Rows[s]["Keep Scrap"] = "No";
                                        }
                                    }
                                    else
                                    {
                                        nn_o = Convert.ToInt32(data_report_outer.Rows[s]["Total number of sections"]);
                                        int[] Pipe_tag = Enumerable.Range(n_o, nn_o).ToArray();
                                        string Pipe_tag_s = string.Join(",", Pipe_tag);
                                        data_report_outer.Rows[s]["48' Pipe Tag"] = Pipe_tag_s;
                                        data_report_outer.Rows[s]["Keep Scrap"] = "No";
                                    }
                                }
                            }
                            else
                            {
                                nn_o = Convert.ToInt32(data_report_outer.Rows[s]["Total number of sections"]);
                                int[] Pipe_tag = Enumerable.Range(n_o, nn_o).ToArray();
                                string Pipe_tag_s = string.Join(",", Pipe_tag);
                                data_report_outer.Rows[s]["48' Pipe Tag"] = Pipe_tag_s;
                                data_report_outer.Rows[s]["Keep Scrap"] = "No";
                            }
                        }
                        else
                        {
                            nn_o = Convert.ToInt32(data_report_outer.Rows[s]["Total number of sections"]);
                            int[] Pipe_tag = Enumerable.Range(n_o, nn_o).ToArray();
                            string Pipe_tag_s = string.Join(",", Pipe_tag);
                            data_report_outer.Rows[s]["48' Pipe Tag"] = Pipe_tag_s;
                            data_report_outer.Rows[s]["Keep Scrap"] = "No Scrap";
                        }
                    }
                }
                data_report_inner.Merge(data_report_outer);
                dataGridView4.DataSource = data_report_inner;
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV Files (*.csv)|*.csv|Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                    Title = "Save Report 2",
                    DefaultExt = "csv",
                    FileName = "Untitled.csv"
                };
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    MessageBox.Show("File will be saved at: " + filePath);
                    ExportToCsv(data_report_inner, filePath);
                }
                else
                {
                    MessageBox.Show("Save operation was cancelled.");
                }
            }

        }

        private List<List<string>> LoadCsv(string filepath)
        {
            List<List<string>> data = new List<List<string>>();
            using (var reader = new StreamReader(filepath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',').ToList();
                    for (int i = 0; i < values.Count; i++)
                    {
                        if (string.IsNullOrEmpty(values[i]) || values[i].Trim().Equals("N/A", StringComparison.OrdinalIgnoreCase))
                        {
                            values[i] = "0";

                        }
                    }
                    data.Add(values);

                }
            }
            return data;
        }
        public static double[,] sortByFirstColumn(double[,] array)
        {
            List<double[]> rows = new List<double[]>();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                double[] row = new double[array.GetLength(1)];
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    row[j] = array[i, j];
                }
                rows.Add(row);
            }
            rows = rows.OrderBy(row => row[0]).ToList();
            double[,] sortedArray = new double[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < rows.Count; i++)
            {
                for (int j = 0; j < rows[i].Length; j++)
                {
                    sortedArray[i, j] = rows[i][j];
                }
            }
            return sortedArray;
        }
        public static double[,] SortArrayByColumn(double[,] data, int columnIndex)
        {
            // Get dimensions of the 2D array
            int rows = data.GetLength(0);
            int cols = data.GetLength(1);
            // Convert 2D array to a list of rows (each row is a double[])
            List<double[]> rowList = new List<double[]>();
            for (int i = 0; i < rows; i++)
            {
                double[] row = new double[cols];
                for (int j = 0; j < cols; j++)
                {
                    row[j] = data[i, j];
                }
                rowList.Add(row);
            }
            // Sort the rows based on the specified column
            rowList = rowList.OrderBy(row => row[columnIndex]).ToList();
            // Convert the sorted list back to a 2D array
            double[,] sortedArray = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    sortedArray[i, j] = rowList[i][j];
                }
            }
            return sortedArray;
        }

        public static DataTable SortDataTableDescending(DataTable table, string columnName)
        {
            return table.AsEnumerable()
                       .OrderByDescending(row => row.Field<double>(columnName))
                       .CopyToDataTable();

        }
        private void DisplayDataInDataGridVIew(List<List<string>> data)
        {
            dataGridView.Columns.Clear();
            dataGridView.Rows.Clear();
            if (data.Count > 0)
            {
                for (int i = 0; i < data[0].Count; i++)
                {
                    dataGridView.Columns.Add($"Column{i + 1}", $"Column{i + 1}");
                }
                foreach (var row in data)
                {
                    dataGridView.Rows.Add(row.ToArray());
                }
            }
        }
        public static void ExportToCsv(DataTable dataTable, string filePath)
        {
            StringBuilder csvContent = new StringBuilder();

            // Add column names (headers) to the CSV
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                csvContent.Append(dataTable.Columns[i].ColumnName);

                if (i < dataTable.Columns.Count - 1)
                {
                    csvContent.Append(",");
                }
            }

            csvContent.AppendLine();  // Move to the next line

            // Add rows to the CSV
            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    // Escape quotes in the data by doubling them
                    string cellValue = row[i].ToString().Replace("\"", "\"\"");

                    // If the value contains a comma, newline, or quote, wrap it in double quotes
                    if (cellValue.Contains(",") || cellValue.Contains("\n") || cellValue.Contains("\""))
                    {
                        cellValue = $"\"{cellValue}\"";
                    }

                    csvContent.Append(cellValue);

                    if (i < dataTable.Columns.Count - 1)
                    {
                        csvContent.Append(",");
                    }
                }

                csvContent.AppendLine();  // Move to the next line after each row
            }

            // Write the CSV content to the file
            File.WriteAllText(filePath, csvContent.ToString());
        }
        //private void DisplayDataInDataGridVIew3(double[,] array)
        //{
            //DataTable table = new DataTable();
            //int rows = array.GetLength(0);
            //int columns= array.GetLength(1);
            //table.Columns.Add("Values", typeof(double));
            //foreach (double item in array)
                //for(int col=0; col<columns; col++) 
                //{
                    //table.Columns.Add($"Column{col + 1}", typeof(double));
                    //table.Rows.Add(item);
                //}
                //for (int row = 0; row < rows; row++)
                //{
                    //DataRow dataRow = table.NewRow();
                    //for (int col = 0; col < columns; col++)
                    //{
                        //dataRow[col] = array[row, col];
                   //}
                //table.Rows.Add(dataRow);
                //}
            //dataGridView3.DataSource = table;

        //}

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
