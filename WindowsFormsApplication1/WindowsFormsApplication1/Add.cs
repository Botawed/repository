using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Add : Form
    {
        public Add()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Объявление родительской формы как свойства дочерней 
        /// </summary>
        private MotionCalculator mainForm
        { 
            get 
            { 
                return this.Owner as MotionCalculator;
            } 
        }

        /// <summary>
        ///  Загрузка формы
        /// </summary>
        private void addMotion_Load(object sender, EventArgs e)
        {   
            if (mainForm.modifyItemFlag != -1) // если при загрузке формы флаг редактирования не равен значению -1, то форма переходи в режим Modify, если нет, то режим Add
            {
                this.Text = "Modify motion"; // в режимы Modify форма называется "Modify motion"
                this.button2.Text = "Modify"; // текст кнопки "Modify"
                if (mainForm.motionList[mainForm.modifyItemFlag] is Model.UniformMotion)
                {
                    radioButton2.Checked = true;
                    maskedTextBox2.Text = Convert.ToString(mainForm.motionList[mainForm.modifyItemFlag].StartingCoordinate);
                    maskedTextBox1.Text = Convert.ToString(mainForm.motionList[mainForm.modifyItemFlag].StartingSpeed);
                    maskedTextBox3.Text = Convert.ToString(mainForm.motionList[mainForm.modifyItemFlag].Time);
                }
                if (mainForm.motionList[mainForm.modifyItemFlag] is Model.AcceleratedMotion)
                {
                    radioButton1.Checked = true;
                     maskedTextBox2.Text = Convert.ToString(mainForm.motionList[mainForm.modifyItemFlag].StartingCoordinate);
                    maskedTextBox1.Text = Convert.ToString(mainForm.motionList[mainForm.modifyItemFlag].StartingSpeed);
                    maskedTextBox3.Text = Convert.ToString(mainForm.motionList[mainForm.modifyItemFlag].Time);
                    if (mainForm.motionList[mainForm.modifyItemFlag].Time != 0)
                        maskedTextBox4.Text = Convert.ToString(2 * (mainForm.motionList[mainForm.modifyItemFlag].CalculationFinishCoordinate() - mainForm.motionList[mainForm.modifyItemFlag].StartingCoordinate) / (mainForm.motionList[mainForm.modifyItemFlag].Time * mainForm.motionList[mainForm.modifyItemFlag].Time));
                    else
                        maskedTextBox4.Text = "0";
                }
                if (mainForm.motionList[mainForm.modifyItemFlag] is Model.VibrationalMotion)
                {
                    radioButton3.Checked = true;
                     maskedTextBox2.Text = Convert.ToString(mainForm.motionList[mainForm.modifyItemFlag].StartingCoordinate);
                    maskedTextBox1.Text = Convert.ToString(mainForm.motionList[mainForm.modifyItemFlag].StartingSpeed);
                    maskedTextBox3.Text = Convert.ToString(mainForm.motionList[mainForm.modifyItemFlag].Time);
                    maskedTextBox5.Text = Convert.ToString((mainForm.motionList[mainForm.modifyItemFlag].Time * mainForm.motionList[mainForm.modifyItemFlag].StartingSpeed) / (mainForm.motionList[mainForm.modifyItemFlag].CalculationFinishCoordinate() - mainForm.motionList[mainForm.modifyItemFlag].StartingCoordinate + 1));
                }
            }
        }

        /// <summary>
        /// Метод для оприделения все ли данные введены для добавления или редактикорования объекта
        /// </summary>
        private void CheckDataComplitness()
        {
            if (radioButton2.Checked == true)
            {
                if (maskedTextBox2.Text == "" || maskedTextBox1.Text == "" || maskedTextBox3.Text == "")
                {
                    button2.Enabled = false; //если данных не достаточно то кнопка добавления не активна
                }
                else button2.Enabled = true;  // кнопка добавления иначе активна
            }
            if (radioButton1.Checked == true)
            {
                if (maskedTextBox2.Text == "" || maskedTextBox1.Text == "" || maskedTextBox3.Text == "" || maskedTextBox4.Text == "")
                {
                    button2.Enabled = false;
                }
                else button2.Enabled = true; 
            }
                if (radioButton3.Checked == true)
                {
                    if (maskedTextBox2.Text == "" || maskedTextBox1.Text == "" || maskedTextBox3.Text == "" || maskedTextBox5.Text == "")
                    {
                        button2.Enabled = false;
                    }
                    else button2.Enabled = true;
                }
            }

        /// <summary>
        /// Локальная переменная для определения типа движения в связи выбором пользователя переключателя
        /// </summary>
        private byte _type;

        private void button2_Click(object sender, EventArgs e)
        {
             
            Model.IMotion motionModel = null; // создание локльной перменной интерфейса
            switch (_type) //  в зависимости от стиля создаем разные объекты
            {
                case 0:  // равноммерное движение
                    motionModel = new Model.UniformMotion(Convert.ToInt32(maskedTextBox2.Text), Convert.ToInt32(maskedTextBox1.Text), Convert.ToInt32(maskedTextBox3.Text));
                    break;
                case 1:  // равноускоренное
                    motionModel = new Model.AcceleratedMotion(Convert.ToInt32(maskedTextBox2.Text), Convert.ToInt32(maskedTextBox1.Text), Convert.ToInt32(maskedTextBox3.Text), Convert.ToInt32(maskedTextBox4.Text));
                    break;
                case 2:  // колебательное
                    motionModel = new Model.VibrationalMotion(Convert.ToInt32(maskedTextBox2.Text), Convert.ToInt32(maskedTextBox1.Text), Convert.ToInt32(maskedTextBox3.Text), Convert.ToInt32(maskedTextBox5.Text));
                    if (Convert.ToInt32(maskedTextBox5.Text) == 0)
                    {
                        MessageBox.Show("Amplitude can not be zero");
                        maskedTextBox5.Text = "";
                        return;
                    }
            break;
            }
            if (mainForm.modifyItemFlag == -1) // если форма открыта в режиме Add, то добавить объект
                mainForm.motionList.Add(motionModel);
            if (mainForm.modifyItemFlag != -1)// если форма открыта в режиме Modify,  то изменить выбранный ранее объект
            {
                mainForm.motionList[mainForm.modifyItemFlag] = motionModel;
                mainForm.modifyItemFlag = -1; 
            }
            this.Close(); // закрыть окно
        }

        /// <summary>
        /// если выбрано равномерное движения
        /// </summary>
        private void UniformCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _type = 0; // uniform
            maskedTextBox4.Text = "";
            maskedTextBox5.Text = "";
           maskedTextBox4.Visible = false;
            label4.Visible = false;
            maskedTextBox5.Visible = false;
            label5.Visible = false;
        }

        /// <summary>
        /// если выбрано равноускоренное движение
        /// </summary>
        private void AcceleratedRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            _type = 1;// accelerated
            maskedTextBox4.Visible = true;
            label4.Visible = true;
            maskedTextBox5.Text = "";
            maskedTextBox5.Visible = false;
            label5.Visible = false;
        }

        /// <summary>
        /// если выбранно колебательное движение
        /// </summary>
        private void VabrationalRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            _type = 2;// vibration
            maskedTextBox4.Text = "";
            maskedTextBox4.Visible = false;
            label4.Visible = false;
            maskedTextBox5.Visible = true;
            label5.Visible = true;
        }

        /// <summary>
        /// Нажатие по кнопке "Close"
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
       {
           mainForm.modifyItemFlag = -1;
           this.Close();
        }
        //проверка на полноту данных для добавления или изменения объекта
        private void maskedTextBox2_TextChanged(object sender, EventArgs e)
        {
            CheckDataComplitness();
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            CheckDataComplitness();
        }

        private void maskedTextBox3_TextChanged(object sender, EventArgs e)
        {
            CheckDataComplitness();
        }

        private void maskedTextBox4_TextChanged(object sender, EventArgs e)
        {
            CheckDataComplitness();
        }

        private void maskedTextBox5_TextChanged(object sender, EventArgs e)
        {
            CheckDataComplitness();
        }       
    }
}
