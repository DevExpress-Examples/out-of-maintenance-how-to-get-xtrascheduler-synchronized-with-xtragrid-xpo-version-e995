// Developer Express Code Central Example:
// How to get XtraScheduler synchronized with XtraGrid (XPO version)
// 
// This example shows how you can synchronize the XtraGrid with the XtraScheduler.
// To accomplish this, handle SelectionChanged events. Within the event handler,
// you should unsubscribe these events, select the appointment representation in
// another control, update selection information and then subscribe to the events
// again. To select the appointment in another control, we have to find the
// selected appointment in the hash table. So, a hash table is created within
// appropriate event handlers. These tables contain associations between XP objects
// and appointments or row handles.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E995

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Native;
using DevExpress.Data;
using System.Collections;
using Scheduler_Grid_XPO.Data;
using DevExpress.Xpo;


namespace Scheduler_Grid_XPO {

    public partial class MainForm : Form {
        Hashtable appointmentHash = new Hashtable();
        Hashtable gridRowHash = new Hashtable();


        protected Hashtable AppointmentHash { get { return appointmentHash; } }
        protected Hashtable GridRowHash { get { return gridRowHash; } }



        public MainForm() {
            InitializeComponent();

            schedulerStorage1.Appointments.DataSource = this.xpAppointments;
            schedulerStorage1.Resources.DataSource = this.xpResources;

            gridControl1.DataSource = this.xpAppointments;

            schedulerStorage1.Appointments.ResourceSharing = true;
            schedulerStorage1.Appointments.Mappings.AllDay = "AllDay";
            schedulerStorage1.Appointments.Mappings.Description = "Description";
            schedulerStorage1.Appointments.Mappings.End = "Finish";
            schedulerStorage1.Appointments.Mappings.Label = "Label";
            schedulerStorage1.Appointments.Mappings.Location = "Location";
            schedulerStorage1.Appointments.Mappings.Start = "Start";
            schedulerStorage1.Appointments.Mappings.Status = "Status";
            schedulerStorage1.Appointments.Mappings.Subject = "Subject";
            schedulerStorage1.Appointments.Mappings.Type = "AppointmentType";
            schedulerStorage1.Appointments.Mappings.ResourceId = "ResourceIds";
            schedulerStorage1.Appointments.Mappings.RecurrenceInfo = "Recurrence";

            schedulerStorage1.Resources.Mappings.Caption = "Name";
            schedulerStorage1.Resources.Mappings.Id = "Oid";
        }

        private void MainForm_Load(object sender, EventArgs e) {
            LoadResources();
            SubscribeSelectionEvents();
            schedulerControl1.Start = DateTime.Today;

        }
        #region Basics
        void LoadResources() {
            ResourceBaseCollection resources = schedulerStorage1.Resources.Items;
            if (resources.Count <= 0) {
                resources.Add(new Resource(0, "Andrew Fuller"));
                resources.Add(new Resource(1, "Nancy Davolio"));
                resources.Add(new Resource(2, "Janet Leverling"));
                resources.Add(new Resource(3, "Margaret Peacock"));
            }
            int count = xpResources.Count;
            for (int i = 0; i < count; i++) {
                XPObject o = (XPObject)xpResources[i];
                o.Save();
            }
        }

        private void OnAppointmentsChanged(object sender, PersistentObjectsEventArgs e) {
            foreach (Appointment apt in e.Objects) {
                XPBaseObject o = apt.GetSourceObject(schedulerStorage1) as XPBaseObject;
                if (o != null)
                    o.Save();
            }
        }
        #endregion

        #region Selection Events
        void SubscribeSelectionEvents() {
            this.schedulerControl1.SelectionChanged += new System.EventHandler(this.schedulerControl1_SelectionChanged);
            this.gridView1.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gridView1_SelectionChanged);
        }

        void UnsubscribeSelectionEvents() {
            this.schedulerControl1.SelectionChanged -= new System.EventHandler(this.schedulerControl1_SelectionChanged);
            this.gridView1.SelectionChanged -= new DevExpress.Data.SelectionChangedEventHandler(this.gridView1_SelectionChanged);
        }
        #endregion

        #region HashTables Populating
        // Fill in the appointment's hash table using the FilterAppointment event.
        private void schedulerStorage1_AppointmentCollectionLoaded(object sender, EventArgs e) {
            foreach (Appointment apt in schedulerStorage1.Appointments.Items) {
                XPAppointment row = (XPAppointment)schedulerStorage1.GetObjectRow(apt);
                if (!AppointmentHash.ContainsKey(row)) {
                    AppointmentHash.Add(row, apt);
                }
                else {
                    AppointmentHash.Remove(row);
                    AppointmentHash.Add(row, apt);

                }
            }
        }

        private void schedulerStorage1_FilterAppointment(object sender, PersistentObjectCancelEventArgs e) {
            Appointment apt0 = e.Object as Appointment;
            Appointment apt = GetAppointmentPattern(apt0);
            XPAppointment row = (XPAppointment)schedulerStorage1.GetObjectRow(apt);
            if(!AppointmentHash.ContainsKey(row)) {
                AppointmentHash.Add(row, apt);
            }
            else {
                AppointmentHash.Remove(row);
                AppointmentHash.Add(row, apt);

            }
        }

        private void schedulerStorage1_AppointmentCollectionCleared(object sender, EventArgs e) {
            AppointmentHash.Clear();
        }
        private void schedulerStorage1_AppointmentCollectionAutoReloading(object sender, CancelListChangedEventArgs e) {
            AppointmentHash.Clear();
        }
        // Fill in the Grid row handle's hash table using the RowCountChanged event.
        private void gridView1_RowCountChanged(object sender, EventArgs e) {
            XPAppointment xpApt;
            for (int i = 0; i < gridView1.RowCount; i++) {
                xpApt = (XPAppointment)gridView1.GetRow(i);
                if (!GridRowHash.ContainsKey(xpApt)) {
                    GridRowHash.Add(xpApt, i);
                }
                else {
                    GridRowHash.Remove(xpApt);
                    GridRowHash.Add(xpApt,i);
                }
            }
        }

        #endregion

        #region Scheduler Selection
        private void schedulerControl1_SelectionChanged(object sender, EventArgs e) {

            UnsubscribeSelectionEvents();
            gridView1.BeginSelection();
            try {
                gridView1.ClearSelection();

                for (int i = 0; i < schedulerControl1.SelectedAppointments.Count; i++) {
                    Appointment apt0 = schedulerControl1.SelectedAppointments[i];
                    Appointment apt = GetAppointmentPattern(apt0);
                    UpdateGridSelection(apt);
                }

            }
            finally {
                gridView1.EndSelection();
                SubscribeSelectionEvents();
            }
        }
        private Appointment GetAppointmentPattern(Appointment apt) {
            switch(apt.Type) {
                case AppointmentType.Occurrence:
                    return apt.RecurrencePattern;
                case AppointmentType.DeletedOccurrence:
                    return apt.RecurrencePattern;
                default:
                    return apt;
            };

            
            
        
        }
        private void UpdateGridSelection(Appointment apt) {
            XPAppointment xpApt = (XPAppointment)apt.GetSourceObject(schedulerStorage1);
            int rowHandle = FindRowHandleByXPAppointment(xpApt);

            gridView1.SelectRow(rowHandle);
            gridView1.MakeRowVisible(rowHandle, false);
        }
        protected int FindRowHandleByXPAppointment(XPAppointment row) {
            return (int)GridRowHash[row];
        }
        #endregion

        #region Grid Selection
        private void gridView1_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            UnsubscribeSelectionEvents();
            try {
                XPAppointment[] gridRows = GetGridSelectedRows();
                AppointmentBaseCollection aptsToSelect = PrepareAppointmentsToSelect(gridRows);
                UpdateSchedulerSelection(aptsToSelect);
            }
            finally {
                SubscribeSelectionEvents();
            }
        }
        XPAppointment[] GetGridSelectedRows() {
            int[] rowHandles = gridView1.GetSelectedRows();
            XPAppointment[] rows = new XPAppointment[rowHandles.Length];
            for (int i = 0; i < rowHandles.Length; i++)
                rows[i] = (XPAppointment)gridView1.GetRow(rowHandles[i]);
            return rows;
        }
        AppointmentBaseCollection PrepareAppointmentsToSelect(XPAppointment[] rows) {
            AppointmentBaseCollection appoitnments = new AppointmentBaseCollection();
            for(int i = 0; i < rows.Length; i++) {
                Appointment apt = FindAppointmentByRow(rows[i]);
                if(apt != null) {
                    switch(apt.Type) {
                        case AppointmentType.Pattern:
                            TimeIntervalCollection tiCollection = schedulerControl1.ActiveView.GetVisibleIntervals();
                            OccurrenceCalculator calc = OccurrenceCalculator.CreateInstance(apt.RecurrenceInfo);
                            AppointmentBaseCollection aptCollection = calc.CalcOccurrences(tiCollection.Interval, apt);
                            appoitnments.AddRange(aptCollection);
                            break;
                        case AppointmentType.DeletedOccurrence:
                            break;
                        default:
                            appoitnments.Add(apt);
                            break;
                    }
                }
            }
            return appoitnments;

        }

        protected Appointment FindAppointmentByRow(XPAppointment row) {
            return AppointmentHash[row] as Appointment;

        }
        void UpdateSchedulerSelection(AppointmentBaseCollection appointments) {
            if (appointments.Count <= 0)
                return;

            SchedulerViewBase view = schedulerControl1.ActiveView;
            view.SelectAppointment(appointments[0]);

            schedulerControl1.BeginUpdate();
            try {
                for (int i = 1; i < appointments.Count; i++)
                    view.AddAppointmentSelection(appointments[i]);

            }
            finally {
                schedulerControl1.EndUpdate();
            }
        }
        #endregion


    }



}