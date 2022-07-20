Public Class FormUtama

   Private Sub KeluarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KeluarToolStripMenuItem.Click
        End
    End Sub

    Private Sub PasienToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasienToolStripMenuItem.Click
        FormPasien.Show()
    End Sub

    Private Sub PegawaiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PegawaiToolStripMenuItem.Click
        FormPegawai.Show()
    End Sub

    Private Sub BerobatToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BerobatToolStripMenuItem.Click
        FormBerobat.Show()
    End Sub
End Class