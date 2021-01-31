using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace coursework0
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Load();
        }

        public List<Teacher> teachers = null; //Список, в который буду записываться найденные элементы во время поиска

        private void Load() //Загрузка в таблицу базы данных
        {
            using ApplicationContext db = new ApplicationContext();
            var teacher = db.Teachers.ToList();
            if (teachers != null)
            {
                DtaGrid.ItemsSource = teachers;
            }
            else
            {
                DtaGrid.ItemsSource = teacher;
            }
        }

        private void Reset_bd(object sender, RoutedEventArgs e) //Обновление базы данных
        {
            teachers = null;
            Load();
        }

        private void Win_Insert(object sender, RoutedEventArgs e) //Добавление поля в бд
        {
            Insert insert = new Insert(); //Создаём окно
            if (insert.ShowDialog() == true) //Показываем окно пока не нажмём на кнопку "Добавить"
            {
                using ApplicationContext db = new ApplicationContext();
                //Получаем значения полей
                string UsSur = insert.Get_Sur;
                string UsName = insert.Get_Name;
                string UsPat = insert.Get_Pat;
                string UsKaf = insert.Get_Kaf;
                string UsSpec = insert.Get_Spec;
                string UsNum = insert.Get_Num;
                string UsBth = insert.Get_Birth;
                string UsAdr = insert.Get_Adr;
                string UsImg = insert.Get_Img;
                //Создаём новый объект
                Teacher teacher = new Teacher { Name = UsName, Surname = UsSur, Patronymic = UsPat, Kaf = UsKaf, Spec = UsSpec, Number = UsNum, Birthday = UsBth, Adres = UsAdr, ImagePath = UsImg };
                db.Teachers.Add(teacher); //Добавляем его в базу
                db.SaveChanges(); //Сохраняем измнения
                Load(); //Загружаем в таблицу обновлённую бд
            }
        }

        private void Info(object sender, RoutedEventArgs e) //Изменение и удаление поля в бд
        {
            using ApplicationContext db = new ApplicationContext();
            if (DtaGrid.SelectedCells.Count == 0) //Проверка на то, выбрано не поле (не сделан ли DoubleClick в "пустой" области)
            {
                return;
            }
            //Выбираем стоблцы таблицы
            var nameInfo = DtaGrid.SelectedCells[0];
            var surInfo = DtaGrid.SelectedCells[1];
            var patInfo = DtaGrid.SelectedCells[2];
            //Получаем значения столбцов в таблице
            string namecontent = (nameInfo.Column.GetCellContent(nameInfo.Item) as TextBlock).Text;
            string surcontent = (surInfo.Column.GetCellContent(surInfo.Item) as TextBlock).Text;
            string patcontent = (patInfo.Column.GetCellContent(patInfo.Item) as TextBlock).Text;
            //Проверяем в базе на наличие совпадений
            var teachers = (from teacher in db.Teachers where teacher.Name == $"{surcontent}" && teacher.Surname == $"{namecontent}" && teacher.Patronymic == $"{patcontent}" select teacher).ToList();
            foreach (Teacher teacher in teachers) //Перебор найденных элементов данных
            {
                Data_Info info = new Data_Info(); //Создаём окно
                //Присваиваем значения элемента формам в окне
                info.surTextBox.Text = teacher.Surname;
                info.nameTextBox.Text = teacher.Name;
                info.patTextBox.Text = teacher.Patronymic;
                info.updateKaf.Text = teacher.Kaf;
                info.updateSpec.Text = teacher.Spec;
                info.birthTexBox.Text = teacher.Birthday;
                info.numTexBox.Text = teacher.Number;
                info.adrTexBox.Text = teacher.Adres;
                info.FileName = teacher.ImagePath;
                if (info.FileName != "") //Проверка на корректность вывода изображения
                {
                    info.Foto.Visibility = Visibility.Hidden;
                    info.Foto_lower.Visibility = Visibility.Visible;
                    info.imgImage.Source =
                        new BitmapImage(new System.Uri(info.FileName));
                }
                if (info.ShowDialog() == true) //Показываем окно пока не нажмутся кнопки "Обновить" или "Удалить"
                {
                    switch (info.Up_Del) //Проверка на выбранное действие: удаление или изменение
                    {
                        case true: //Изменение
                            //Получаем значения из форм
                            string UsName = info.Get_Name;
                            string UsSurname = info.Get_Sur;
                            string UsPatronymic = info.Get_Pat;
                            string UsKaf = info.Update_Kaf;
                            string UsSpec = info.Update_Spec;
                            string UsNum = info.Get_Num;
                            string UsBth = info.Get_Bth;
                            string UsAdr = info.Get_Adr;
                            string UsImg = info.FileName;
                            //Присваиваем значения полям элемента
                            teacher.Name = UsName;
                            teacher.Surname = UsSurname;
                            teacher.Patronymic = UsPatronymic;
                            teacher.Kaf = UsKaf;
                            teacher.Spec = UsSpec;
                            teacher.Number = UsNum;
                            teacher.Birthday = UsBth;
                            teacher.Adres = UsAdr;
                            teacher.ImagePath = UsImg;
                            break;
                        case false: //Удаление
                            db.Teachers.Remove(teacher);
                            break;
                    }
                }
            }
            db.SaveChanges(); //Обновляем данные
            Load(); //Загружаем изменённую бд
        }

        private void Search_bd(object sender, RoutedEventArgs e) //Поиск элемента в бд
        {
            using ApplicationContext db = new ApplicationContext();
            Search search = new Search(); //Создаём окно
            if (search.ShowDialog() == true) //Показываем окно пока не нажмём кнопку "Поиск"
            {
                //Получаем значения для поиска из форм
                string value = search.search_name; //Значение для поиска
                string criterion = search.Get_Text; //Критерий поиска, иначе - название столбца (ФИО, Кафедра, Должность)
                string[] name = criterion.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); //Разбивает строку на слова (необходимо при поиске по ФИО)
                switch (value) //Выбираем один из критериев
                {
                    case "ФИО":
                        if (name.Length == 1) //Если как значение передаётся только одно слово (Имя или Фамилия или Отчество)
                        {
                            //Ищем в базе
                            teachers = (from teacher in db.Teachers where (teacher.Surname == $"{name[0]}") || (teacher.Name == $"{name[0]}") || (teacher.Patronymic == $"{name[0]}") select teacher).ToList();
                        }
                        else if (name.Length == 2) //Если как значение передаются два слова (Имя и Фамилия или Отчество и Имя)
                        {
                            teachers = (from teacher in db.Teachers where teacher.Surname == $"{name[0]}" && teacher.Name == $"{name[1]}" select teacher).ToList();
                            if (teachers.Count == 0) //Если не найдено по Имя-Фамилия, ищем по Имя-Отчество
                            {
                                teachers = (from teacher in db.Teachers where teacher.Name == $"{name[0]}" && teacher.Patronymic == $"{name[1]}" select teacher).ToList();
                            }
                        }
                        else if (name.Length == 3) //Если как значение передаются три слова (Имя, Фамилия и Отчество)
                        {
                            teachers = (from teacher in db.Teachers where teacher.Surname == $"{name[0]}" && teacher.Name == $"{name[1]}" && teacher.Patronymic == $"{name[2]}" select teacher).ToList();
                        }
                        break;
                    case "Кафедра":
                        teachers = (from teacher in db.Teachers where teacher.Kaf == $"{criterion}" select teacher).ToList();
                        break;
                    case "Должность":
                        teachers = (from teacher in db.Teachers where teacher.Spec == $"{criterion}" select teacher).ToList();
                        break;
                }
                DtaGrid.ItemsSource = teachers; //Выводим найденные(или нет) результаты на главное окно
            }
        }
    }
}
