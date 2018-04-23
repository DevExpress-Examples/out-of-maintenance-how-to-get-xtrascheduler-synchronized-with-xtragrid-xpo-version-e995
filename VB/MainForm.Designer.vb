Namespace Scheduler_Grid_XPO
    Partial Public Class MainForm
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        #Region "Windows Form Designer generated code"

        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim timeRuler1 As New DevExpress.XtraScheduler.TimeRuler()
            Dim timeRuler2 As New DevExpress.XtraScheduler.TimeRuler()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.label1 = New System.Windows.Forms.Label()
            Me.schedulerControl1 = New DevExpress.XtraScheduler.SchedulerControl()
            Me.schedulerStorage1 = New DevExpress.XtraScheduler.SchedulerStorage(Me.components)
            Me.xpAppointments = New DevExpress.Xpo.XPCollection()
            Me.gridControl1 = New DevExpress.XtraGrid.GridControl()
            Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
            Me.colStatus = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.colSubject = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.colDescription = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.colLabel = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.colStartTime = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.colEndTime = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.colLocation = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.colAllDay = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.xpResources = New DevExpress.Xpo.XPCollection()
            Me.panel1.SuspendLayout()
            DirectCast(Me.schedulerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.schedulerStorage1, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.xpAppointments, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.gridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.xpResources, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            ' 
            ' panel1
            ' 
            Me.panel1.Controls.Add(Me.label1)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(707, 24)
            Me.panel1.TabIndex = 13
            ' 
            ' label1
            ' 
            Me.label1.AutoSize = True
            Me.label1.Location = New System.Drawing.Point(4, 4)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(383, 13)
            Me.label1.TabIndex = 0
            Me.label1.Text = "Create new appointments. Click to select grid rows and scheduler appointments."
            ' 
            ' schedulerControl1
            ' 
            Me.schedulerControl1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Month
            Me.schedulerControl1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.schedulerControl1.Location = New System.Drawing.Point(0, 0)
            Me.schedulerControl1.Name = "schedulerControl1"
            Me.schedulerControl1.Size = New System.Drawing.Size(707, 315)
            Me.schedulerControl1.Start = New Date(2005, 7, 4, 0, 0, 0, 0)
            Me.schedulerControl1.Storage = Me.schedulerStorage1
            Me.schedulerControl1.TabIndex = 11
            Me.schedulerControl1.Text = "schedulerControl1"
            Me.schedulerControl1.Views.DayView.NavigationButtonVisibility = DevExpress.XtraScheduler.NavigationButtonVisibility.Never
            Me.schedulerControl1.Views.DayView.TimeRulers.Add(timeRuler1)
            Me.schedulerControl1.Views.MonthView.NavigationButtonVisibility = DevExpress.XtraScheduler.NavigationButtonVisibility.Never
            Me.schedulerControl1.Views.MonthView.WeekCount = 3
            Me.schedulerControl1.Views.TimelineView.NavigationButtonVisibility = DevExpress.XtraScheduler.NavigationButtonVisibility.Never
            Me.schedulerControl1.Views.WeekView.NavigationButtonVisibility = DevExpress.XtraScheduler.NavigationButtonVisibility.Never
            Me.schedulerControl1.Views.WorkWeekView.NavigationButtonVisibility = DevExpress.XtraScheduler.NavigationButtonVisibility.Never
            Me.schedulerControl1.Views.WorkWeekView.TimeRulers.Add(timeRuler2)
            ' 
            ' schedulerStorage1
            ' 
            Me.schedulerStorage1.Appointments.DataSource = Me.xpAppointments
            Me.schedulerStorage1.Appointments.Mappings.AllDay = "AllDay"
            Me.schedulerStorage1.Appointments.Mappings.Description = "Description"
            Me.schedulerStorage1.Appointments.Mappings.End = "EndTime"
            Me.schedulerStorage1.Appointments.Mappings.Label = "Label"
            Me.schedulerStorage1.Appointments.Mappings.Location = "Location"
            Me.schedulerStorage1.Appointments.Mappings.Start = "StartTime"
            Me.schedulerStorage1.Appointments.Mappings.Status = "Status"
            Me.schedulerStorage1.Appointments.Mappings.Subject = "Subject"
            ' 
            ' xpAppointments
            ' 
            Me.xpAppointments.DeleteObjectOnRemove = True
            Me.xpAppointments.DisplayableProperties = "This;Oid;AllDay;Description;Finish;Label;Location;Recurrence;Reminder;Start;Statu" & "s;Subject;AppointmentType;Resources;ResourceIds"
            Me.xpAppointments.ObjectType = GetType(Scheduler_Grid_XPO.Data.XPAppointment)
            ' 
            ' gridControl1
            ' 
            Me.gridControl1.DataSource = Me.xpAppointments
            Me.gridControl1.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.gridControl1.Location = New System.Drawing.Point(0, 315)
            Me.gridControl1.MainView = Me.gridView1
            Me.gridControl1.Name = "gridControl1"
            Me.gridControl1.Size = New System.Drawing.Size(707, 233)
            Me.gridControl1.TabIndex = 12
            Me.gridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.gridView1})
            ' 
            ' gridView1
            ' 
            Me.gridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() { Me.colStatus, Me.colSubject, Me.colDescription, Me.colLabel, Me.colStartTime, Me.colEndTime, Me.colLocation, Me.colAllDay})
            Me.gridView1.GridControl = Me.gridControl1
            Me.gridView1.Name = "gridView1"
            Me.gridView1.OptionsSelection.MultiSelect = True
            ' 
            ' colStatus
            ' 
            Me.colStatus.Caption = "Status"
            Me.colStatus.FieldName = "Status"
            Me.colStatus.Name = "colStatus"
            Me.colStatus.Visible = True
            Me.colStatus.VisibleIndex = 0
            ' 
            ' colSubject
            ' 
            Me.colSubject.Caption = "Subject"
            Me.colSubject.FieldName = "Subject"
            Me.colSubject.Name = "colSubject"
            Me.colSubject.Visible = True
            Me.colSubject.VisibleIndex = 1
            ' 
            ' colDescription
            ' 
            Me.colDescription.Caption = "Description"
            Me.colDescription.FieldName = "Description"
            Me.colDescription.Name = "colDescription"
            Me.colDescription.Visible = True
            Me.colDescription.VisibleIndex = 2
            ' 
            ' colLabel
            ' 
            Me.colLabel.Caption = "Label"
            Me.colLabel.FieldName = "Label"
            Me.colLabel.Name = "colLabel"
            Me.colLabel.Visible = True
            Me.colLabel.VisibleIndex = 3
            ' 
            ' colStartTime
            ' 
            Me.colStartTime.Caption = "StartTime"
            Me.colStartTime.FieldName = "StartTime"
            Me.colStartTime.Name = "colStartTime"
            Me.colStartTime.Visible = True
            Me.colStartTime.VisibleIndex = 4
            ' 
            ' colEndTime
            ' 
            Me.colEndTime.Caption = "EndTime"
            Me.colEndTime.FieldName = "EndTime"
            Me.colEndTime.Name = "colEndTime"
            Me.colEndTime.Visible = True
            Me.colEndTime.VisibleIndex = 5
            ' 
            ' colLocation
            ' 
            Me.colLocation.Caption = "Location"
            Me.colLocation.FieldName = "Location"
            Me.colLocation.Name = "colLocation"
            Me.colLocation.Visible = True
            Me.colLocation.VisibleIndex = 6
            ' 
            ' colAllDay
            ' 
            Me.colAllDay.Caption = "AllDay"
            Me.colAllDay.FieldName = "AllDay"
            Me.colAllDay.Name = "colAllDay"
            Me.colAllDay.Visible = True
            Me.colAllDay.VisibleIndex = 7
            ' 
            ' xpResources
            ' 
            Me.xpResources.DeleteObjectOnRemove = True
            Me.xpResources.DisplayableProperties = "This;Oid;Name;Appointments"
            Me.xpResources.ObjectType = GetType(Scheduler_Grid_XPO.Data.XPResource)
            ' 
            ' MainForm
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(707, 548)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me.schedulerControl1)
            Me.Controls.Add(Me.gridControl1)
            Me.Name = "MainForm"
            Me.Text = "Scheduler_Grid_XPO"
            Me.panel1.ResumeLayout(False)
            Me.panel1.PerformLayout()
            DirectCast(Me.schedulerControl1, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.schedulerStorage1, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.xpAppointments, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.gridControl1, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.xpResources, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

        #End Region

        Private panel1 As System.Windows.Forms.Panel
        Private label1 As System.Windows.Forms.Label
        Private schedulerControl1 As DevExpress.XtraScheduler.SchedulerControl
        Private gridControl1 As DevExpress.XtraGrid.GridControl
        Private WithEvents gridView1 As DevExpress.XtraGrid.Views.Grid.GridView
        Private colStatus As DevExpress.XtraGrid.Columns.GridColumn
        Private colSubject As DevExpress.XtraGrid.Columns.GridColumn
        Private colDescription As DevExpress.XtraGrid.Columns.GridColumn
        Private colLabel As DevExpress.XtraGrid.Columns.GridColumn
        Private colStartTime As DevExpress.XtraGrid.Columns.GridColumn
        Private colEndTime As DevExpress.XtraGrid.Columns.GridColumn
        Private colLocation As DevExpress.XtraGrid.Columns.GridColumn
        Private colAllDay As DevExpress.XtraGrid.Columns.GridColumn
        Private WithEvents schedulerStorage1 As DevExpress.XtraScheduler.SchedulerStorage
        Private xpAppointments As DevExpress.Xpo.XPCollection
        Private xpResources As DevExpress.Xpo.XPCollection
    End Class
End Namespace