﻿Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Scheduler_Grid_XPO
    Friend NotInheritable Class Program

        Private Sub New()
        End Sub

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread> _
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            DevExpress.Skins.SkinManager.EnableFormSkins()
            Application.Run(New MainForm())
        End Sub
    End Class
End Namespace
