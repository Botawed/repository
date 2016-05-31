using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft;
using Newtonsoft.Json;


namespace WindowsFormsApplication1
{
    /// <summary>
    /// Главная форма калькулятора
    /// </summary>
    public partial class MotionCalculator : Form
    {
        /// <summary>
        /// Конструктор формы
        /// </summary>
        public MotionCalculator()
        {
            InitializeComponent();
             #if !DEBUG // если открыто не в дебаге
            CreateRandomDataButton.Visible = false; // сделать кнопку генерации объектов со случайными данными невидимой, т.е. недоступной пользователю
            #endif // конец условия препроцессора
            foreach (DataGridViewColumn column in dataGridView1.Columns) // запретить сортировку по столбцам столбцов
                {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        /// <summary>
        /// Список объектов движения
        /// </summary>
        public List<Model.IMotion> motionList = new List<Model.IMotion>();

        /// <summary>
        /// Строка выбранная для редактирования в таблице
        /// </summary>
        public int modifyItemFlag = -1;
       
        /// <summary>
        /// Переменная для храниения выбранной строки в таблице 
        /// </summary>
        public int selectedRowIndex;

        
        /// <summary>
        /// Нажатие на кнопку "Remove motion"
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                selectedRowIndex = dataGridView1.SelectedRows[0].Index; // запоминаем выбранную строку т.к. после вызова диалогового окна выбранная строка "слетает"
            }

            if (dataGridView1.Rows.Count != 0) // если таблица не пуста 
            {
                DialogResult result = MessageBox.Show("Remove the row and item?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question); // вывести сообщение с вопросом: действительно ли пользователь хочет удалить запись
                if (result == DialogResult.Yes) // если пользователь отвечает да 
                {
                    motionList.RemoveAt(selectedRowIndex);             // удалить объект из списка по индексу выделенной в таблице строки
                    dataGridView1.Rows.RemoveAt(selectedRowIndex);     // удалить выденную строку из таблицы     
                }
            }
            else MessageBox.Show("Table is empty!"); // если выбрана пустая строка
        }

        /// <summary>
        /// Нажатие по кнопке "Add"
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            var addMotion = new Add();// инициализовать форму для добавление объекта в список и таблицу
            addMotion.Owner = this; // указываем родителя формы
            addMotion.ShowDialog(); // открыть форму addMotion поверх MotionCalculator
        }

        /// <summary>
        /// Метод для перерисовки таблицы при активации формы MotionCalculation
        /// </summary>
        private void MotionCalculator_Activated(object sender, EventArgs e)
        {
            {
                if (motionList.Count != 0) // если список объектов не пуст
                {
                    dataGridView1.Rows.Clear(); // очистить таблицу
                    for (int i = 0; i < motionList.Count; i++) // перебираем все объекты из списка и добавляем в таблицу
                    {
                        dataGridView1.Rows.Add(motionList[i].Type,motionList[i].StartingSpeed, motionList[i].StartingCoordinate,
                         motionList[i].Time, motionList[i].CalculationFinishCoordinate());
                    }
                }
            }
        }
    
        /// <summary>
        /// Нажание по кнопке Modify
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                var ModifyMotion = new Add();// инициализовать форму для добавление объекта в список и таблицу
                modifyItemFlag = dataGridView1.SelectedRows[0].Index; // запомнить выделенную строку в таблице
                ModifyMotion.Owner = this; // указываем родителя формы
                ModifyMotion.ShowDialog(); // открыть окно поверх MotionCalculator
            }
            else MessageBox.Show("Table is empty");
        }

        /// <summary>
        /// Нажатие на кнопу в меню "New" в главном меню
        /// </summary>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Current data will be lost. Сontinue?", "New", MessageBoxButtons.YesNo, MessageBoxIcon.Question); // выводим сообщение с вопросом
            if (result == DialogResult.Yes) // если пользователь отвечает да 
            {
                motionList.Clear();             // очистить список объектов
                dataGridView1.Rows.Clear();     // очистить таблицу    
            }

        }


        /// <summary>
        /// Нажатие по кнопке "Save As" в главном меню
        /// </summary>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var serializer = new Newtonsoft.Json.JsonSerializer()
            {
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };
            var saveFileDialog = new SaveFileDialog();
            var fileName = saveFileDialog.FileName;
            saveFileDialog.Filter = "Motion | *.motion";
            saveFileDialog.OverwritePrompt = true;
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            string outputString = Newtonsoft.Json.JsonConvert.SerializeObject(motionList);
            using (StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName))
            {
                using (Newtonsoft.Json.JsonWriter jWriter = new Newtonsoft.Json.JsonTextWriter(streamWriter))
                {
                    serializer.Serialize(jWriter, motionList);
                }
            }
            MessageBox.Show("File is saved");
        }


        /// <summary>
        /// Нажатие по клавише "Load" в главном меню
        /// </summary>
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                var serializer = new Newtonsoft.Json.JsonSerializer()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    TypeNameHandling = TypeNameHandling.Auto,
                    Formatting = Formatting.Indented,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };
                var openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Motion data|*.motion";
                if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                    return;
                using (StreamReader streamReader = new StreamReader(openFileDialog.FileName))
                {
                    using (Newtonsoft.Json.JsonReader jReader = new Newtonsoft.Json.JsonTextReader(streamReader))
                    {
                        motionList = serializer.Deserialize<List<Model.IMotion>>(jReader);
                    }
                }
                MessageBox.Show("File is loading");
            }
        }
    }
}
