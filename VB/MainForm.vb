' Developer Express Code Central Example:
' How to get XtraScheduler synchronized with XtraGrid (XPO version)
' 
' This example shows how you can synchronize the XtraGrid with the XtraScheduler.
' To accomplish this, handle SelectionChanged events. Within the event handler,
' you should unsubscribe these events, select the appointment representation in
' another control, update selection information and then subscribe to the events
' again. To select the appointment in another control, we have to find the
' selected appointment in the hash table. So, a hash table is created within
' appropriate event handlers. These tables contain associations between XP objects
' and appointments or row handles.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E995

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Native
Imports DevExpress.Data
Imports System.Collections
Imports Scheduler_Grid_XPO.Data
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB


Namespace Scheduler_Grid_XPO

    Partial Public Class MainForm
        Inherits Form


        Private appointmentHash_Renamed As New Hashtable()

        Private gridRowHash_Renamed As New Hashtable()


        Protected ReadOnly Property AppointmentHash() As Hashtable
            Get
                Return appointmentHash_Renamed
            End Get
        End Property
        Protected ReadOnly Property GridRowHash() As Hashtable
            Get
                Return gridRowHash_Renamed
            End Get
        End Property



        Public Sub New()
            XpoDefault.DataLayer = New SimpleDataLayer(New InMemoryDataStore())

            InitializeComponent()

            schedulerStorage1.Appointments.DataSource = Me.xpAppointments
            schedulerStorage1.Resources.DataSource = Me.xpResources

            gridControl1.DataSource = Me.xpAppointments

            schedulerStorage1.Appointments.ResourceSharing = True
            schedulerStorage1.Appointments.Mappings.AllDay = "AllDay"
            schedulerStorage1.Appointments.Mappings.Description = "Description"
            schedulerStorage1.Appointments.Mappings.End = "Finish"
            schedulerStorage1.Appointments.Mappings.Label = "Label"
            schedulerStorage1.Appointments.Mappings.Location = "Location"
            schedulerStorage1.Appointments.Mappings.Start = "Start"
            schedulerStorage1.Appointments.Mappings.Status = "Status"
            schedulerStorage1.Appointments.Mappings.Subject = "Subject"
            schedulerStorage1.Appointments.Mappings.Type = "AppointmentType"
            schedulerStorage1.Appointments.Mappings.ResourceId = "ResourceIds"
            schedulerStorage1.Appointments.Mappings.RecurrenceInfo = "Recurrence"

            schedulerStorage1.Resources.Mappings.Caption = "Name"
            schedulerStorage1.Resources.Mappings.Id = "Oid"
        End Sub

        Private Sub MainForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            LoadResources()
            SubscribeSelectionEvents()
            schedulerControl1.Start = Date.Today

        End Sub
        #Region "Basics"
        Private Sub LoadResources()
            Dim resources As ResourceBaseCollection = schedulerStorage1.Resources.Items
            If resources.Count <= 0 Then
                resources.Add(Me.schedulerStorage1.CreateResource(0, "Andrew Fuller"))
                resources.Add(Me.schedulerStorage1.CreateResource(1, "Nancy Davolio"))
                resources.Add(Me.schedulerStorage1.CreateResource(2, "Janet Leverling"))
                resources.Add(Me.schedulerStorage1.CreateResource(3, "Margaret Peacock"))
            End If
            Dim count As Integer = xpResources.Count
            For i As Integer = 0 To count - 1
                Dim o As XPObject = CType(xpResources(i), XPObject)
                o.Save()
            Next i
        End Sub

        Private Sub OnAppointmentsChanged(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs) Handles schedulerStorage1.AppointmentsChanged
            For Each apt As Appointment In e.Objects
                Dim o As XPBaseObject = TryCast(apt.GetSourceObject(schedulerStorage1), XPBaseObject)
                If o IsNot Nothing Then
                    o.Save()
                End If
            Next apt
        End Sub
        #End Region

        #Region "Selection Events"
        Private Sub SubscribeSelectionEvents()
            AddHandler schedulerControl1.SelectionChanged, AddressOf schedulerControl1_SelectionChanged
            AddHandler gridView1.SelectionChanged, AddressOf gridView1_SelectionChanged
        End Sub

        Private Sub UnsubscribeSelectionEvents()
            RemoveHandler schedulerControl1.SelectionChanged, AddressOf schedulerControl1_SelectionChanged
            RemoveHandler gridView1.SelectionChanged, AddressOf gridView1_SelectionChanged
        End Sub
        #End Region

        #Region "HashTables Populating"
        ' Fill in the appointment's hash table using the FilterAppointment event.
        Private Sub schedulerStorage1_AppointmentCollectionLoaded(ByVal sender As Object, ByVal e As EventArgs) Handles schedulerStorage1.AppointmentCollectionLoaded
            For Each apt As Appointment In schedulerStorage1.Appointments.Items
                Dim row As XPAppointment = CType(schedulerStorage1.GetObjectRow(apt), XPAppointment)
                If Not AppointmentHash.ContainsKey(row) Then
                    AppointmentHash.Add(row, apt)
                Else
                    AppointmentHash.Remove(row)
                    AppointmentHash.Add(row, apt)

                End If
            Next apt
        End Sub

        Private Sub schedulerStorage1_FilterAppointment(ByVal sender As Object, ByVal e As PersistentObjectCancelEventArgs) Handles schedulerStorage1.FilterAppointment
            Dim apt0 As Appointment = TryCast(e.Object, Appointment)
            Dim apt As Appointment = GetAppointmentPattern(apt0)
            Dim row As XPAppointment = CType(schedulerStorage1.GetObjectRow(apt), XPAppointment)
            If Not AppointmentHash.ContainsKey(row) Then
                AppointmentHash.Add(row, apt)
            Else
                AppointmentHash.Remove(row)
                AppointmentHash.Add(row, apt)

            End If
        End Sub

        Private Sub schedulerStorage1_AppointmentCollectionCleared(ByVal sender As Object, ByVal e As EventArgs) Handles schedulerStorage1.AppointmentCollectionCleared
            AppointmentHash.Clear()
        End Sub
        Private Sub schedulerStorage1_AppointmentCollectionAutoReloading(ByVal sender As Object, ByVal e As CancelListChangedEventArgs) Handles schedulerStorage1.AppointmentCollectionAutoReloading
            AppointmentHash.Clear()
        End Sub
        ' Fill in the Grid row handle's hash table using the RowCountChanged event.
        Private Sub gridView1_RowCountChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gridView1.RowCountChanged
            Dim xpApt As XPAppointment
            For i As Integer = 0 To gridView1.RowCount - 1
                xpApt = CType(gridView1.GetRow(i), XPAppointment)
                If Not GridRowHash.ContainsKey(xpApt) Then
                    GridRowHash.Add(xpApt, i)
                Else
                    GridRowHash.Remove(xpApt)
                    GridRowHash.Add(xpApt,i)
                End If
            Next i
        End Sub

        #End Region

        #Region "Scheduler Selection"
        Private Sub schedulerControl1_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs)

            UnsubscribeSelectionEvents()
            gridView1.BeginSelection()
            Try
                gridView1.ClearSelection()

                For i As Integer = 0 To schedulerControl1.SelectedAppointments.Count - 1
                    Dim apt0 As Appointment = schedulerControl1.SelectedAppointments(i)
                    Dim apt As Appointment = GetAppointmentPattern(apt0)
                    UpdateGridSelection(apt)
                Next i

            Finally
                gridView1.EndSelection()
                SubscribeSelectionEvents()
            End Try
        End Sub
        Private Function GetAppointmentPattern(ByVal apt As Appointment) As Appointment
            Select Case apt.Type
                Case AppointmentType.Occurrence
                    Return apt.RecurrencePattern
                Case AppointmentType.DeletedOccurrence
                    Return apt.RecurrencePattern
                Case Else
                    Return apt
            End Select




        End Function
        Private Sub UpdateGridSelection(ByVal apt As Appointment)
            Dim xpApt As XPAppointment = CType(apt.GetSourceObject(schedulerStorage1), XPAppointment)
            Dim rowHandle As Integer = FindRowHandleByXPAppointment(xpApt)

            gridView1.SelectRow(rowHandle)
            gridView1.MakeRowVisible(rowHandle, False)
        End Sub
        Protected Function FindRowHandleByXPAppointment(ByVal row As XPAppointment) As Integer
            Return DirectCast(GridRowHash(row), Integer)
        End Function
        #End Region

        #Region "Grid Selection"
        Private Sub gridView1_SelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
            UnsubscribeSelectionEvents()
            Try
                Dim gridRows() As XPAppointment = GetGridSelectedRows()
                Dim aptsToSelect As AppointmentBaseCollection = PrepareAppointmentsToSelect(gridRows)
                UpdateSchedulerSelection(aptsToSelect)
            Finally
                SubscribeSelectionEvents()
            End Try
        End Sub
        Private Function GetGridSelectedRows() As XPAppointment()
            Dim rowHandles() As Integer = gridView1.GetSelectedRows()
            Dim rows(rowHandles.Length - 1) As XPAppointment
            For i As Integer = 0 To rowHandles.Length - 1
                rows(i) = CType(gridView1.GetRow(rowHandles(i)), XPAppointment)
            Next i
            Return rows
        End Function
        Private Function PrepareAppointmentsToSelect(ByVal rows() As XPAppointment) As AppointmentBaseCollection
            Dim appoitnments As New AppointmentBaseCollection()
            For i As Integer = 0 To rows.Length - 1
                Dim apt As Appointment = FindAppointmentByRow(rows(i))
                If apt IsNot Nothing Then
                    Select Case apt.Type
                        Case AppointmentType.Pattern
                            Dim tiCollection As TimeIntervalCollection = schedulerControl1.ActiveView.GetVisibleIntervals()
                            Dim calc As OccurrenceCalculator = OccurrenceCalculator.CreateInstance(apt.RecurrenceInfo)
                            Dim aptCollection As AppointmentBaseCollection = calc.CalcOccurrences(tiCollection.Interval, apt)
                            appoitnments.AddRange(aptCollection)
                        Case AppointmentType.DeletedOccurrence
                        Case Else
                            appoitnments.Add(apt)
                    End Select
                End If
            Next i
            Return appoitnments

        End Function

        Protected Function FindAppointmentByRow(ByVal row As XPAppointment) As Appointment
            Return TryCast(AppointmentHash(row), Appointment)

        End Function
        Private Sub UpdateSchedulerSelection(ByVal appointments As AppointmentBaseCollection)
            If appointments.Count <= 0 Then
                Return
            End If

            Dim view As SchedulerViewBase = schedulerControl1.ActiveView
            view.SelectAppointment(appointments(0))

            schedulerControl1.BeginUpdate()
            Try
                For i As Integer = 1 To appointments.Count - 1
                    view.AddAppointmentSelection(appointments(i))
                Next i

            Finally
                schedulerControl1.EndUpdate()
            End Try
        End Sub
        #End Region


    End Class



End Namespace