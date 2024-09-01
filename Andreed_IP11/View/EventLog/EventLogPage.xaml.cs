using Andreed_IP11.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static Andreed_IP11.View.CarRepair.RedactCarRepairPage;

namespace Andreed_IP11.View.EventLog
{
    /// <summary>
    /// Логика взаимодействия для EventLogPage.xaml
    /// </summary>
    public partial class EventLogPage : Page
    {
        Core db = new Core();
        int selectedId;
        public EventLogPage()
        {
            InitializeComponent();
            Update();
            LoadNames();
        }
        private void LoadNames()
        {
            List<PartInfof> partsList = db.context.Users
               .Select(p => new PartInfof { Id = p.UserID, Name = p.Username })
               .ToList();
            UserFilterComboBox.ItemsSource = partsList;
            UserFilterComboBox.DisplayMemberPath = "Name";
            UserFilterComboBox.SelectedValuePath = "Id";
        }

        private void ApplyFilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserFilterComboBox.Text != "" && (StartDatePicker.Text == "" && EndDatePicker.Text == ""))
            {
                EventLogDataGrid.ItemsSource = null;
                var eventLogs = from eventLog in db.context.EventsLog
                                join user in db.context.Users on eventLog.UserID equals user.UserID
                                where eventLog.UserID == selectedId
                                select new
                                {
                                    EventDateTime = eventLog.EventDate,
                                    UserName = user.Username,
                                    EventDescription = eventLog.EventDescription
                                };

                EventLogDataGrid.ItemsSource = eventLogs.ToList();
            }

            else if (StartDatePicker.Text != "" && EndDatePicker.Text != "" && UserFilterComboBox.Text == "")
            {
                EventLogDataGrid.ItemsSource = null;

                DateTime startDate = StartDatePicker.SelectedDate ?? DateTime.MinValue;
                DateTime endDate = EndDatePicker.SelectedDate ?? DateTime.MinValue;
                // MessageBox.Show(startDate.ToString() + " " + endDate.ToString());

                endDate = endDate.AddDays(1);

                var eventLogs = from eventLog in db.context.EventsLog
                                join user in db.context.Users on eventLog.UserID equals user.UserID
                                where eventLog.EventDate >= startDate && eventLog.EventDate <= endDate
                                select new
                                {
                                    EventDateTime = eventLog.EventDate,
                                    UserName = user.Username,
                                    EventDescription = eventLog.EventDescription
                                };

                EventLogDataGrid.ItemsSource = eventLogs.ToList();
            }

            else if ((StartDatePicker.Text != "" && EndDatePicker.Text != "") && UserFilterComboBox.Text != "")
            {
                EventLogDataGrid.ItemsSource = null;

                DateTime startDate = StartDatePicker.SelectedDate ?? DateTime.MinValue;
                DateTime endDate = EndDatePicker.SelectedDate ?? DateTime.MinValue;
                // MessageBox.Show(startDate.ToString() + " " + endDate.ToString());

                endDate = endDate.AddDays(1);

                var eventLogs = from eventLog in db.context.EventsLog
                                join user in db.context.Users on eventLog.UserID equals user.UserID
                                where eventLog.EventDate >= startDate && eventLog.EventDate <= endDate && eventLog.UserID == selectedId
                                select new
                                {
                                    EventDateTime = eventLog.EventDate,
                                    UserName = user.Username,
                                    EventDescription = eventLog.EventDescription
                                };

                EventLogDataGrid.ItemsSource = eventLogs.ToList();
            }
            else
            {
                MessageBox.Show("Не выбраны фильтры");
            }
        }
        bool checkclear = true;
        private void ClearFilterButton_Click(object sender, RoutedEventArgs e)
        {
            checkclear = false;
            EventLogDataGrid.ItemsSource = null;
            UserFilterComboBox.Text = "";
            StartDatePicker.Text = "";
            EndDatePicker.Text = "";
            Update();
        }

        private void Update()
        {
            var eventLogs = from eventLog in db.context.EventsLog
                            join user in db.context.Users on eventLog.UserID equals user.UserID
                            select new
                            {
                                EventDateTime = eventLog.EventDate,
                                UserName = user.Username,
                                EventDescription = eventLog.EventDescription
                            };

            EventLogDataGrid.ItemsSource = eventLogs.ToList();

        }

        private void UserFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (checkclear == true)
            {
                selectedId = (int)UserFilterComboBox.SelectedValue;
                //MessageBox.Show(selectedId.ToString());
            }
            else
            {
                checkclear = true;
            }
        }
    }
}
