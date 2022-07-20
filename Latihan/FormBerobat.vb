
Imports MySql.Data.MySqlClient
Public Class FormBerobat
    Public StrSql As String
    Public db As MySqlConnection
    Public cmd, cmdl As MySqlCommand
    Public dataadapter As MySqlDataAdapter
    Public datardr As MySqlDataReader
    Public ds As DataSet
    Dim conn As AksesData.koneksiDB
    Private Sub ComboPasien()
        Call bukaDB()
        cmd = New MySqlCommand("SElECT * From pasien", Koneksi)
        datardr = cmd.ExecuteReader
        Do While datardr.Read
            CboKode_Pasien.Items.Add(datardr("Kode_Pasien"))
        Loop
        datardr.Close()
    End Sub
    Private Sub ComboPegawai()
        Call bukaDB()
        cmd = New MySqlCommand("SELECT * From Pegawai", Koneksi)
        datardr = cmd.ExecuteReader
        Do While datardr.Read
            CboNama_Pegawai.Items.Add(datardr("Nama_Pegawai"))
        Loop
        datardr.Close()
    End Sub
    Private Sub BersihData()
        txtNoBukti.Text = ""
        txtTglDaftar.Text = ""
        txtNama_Pasien.Text = ""
        CboKode_Pasien.Text = ""
        txtNamaObat.Text = ""
        txtHarga.Text = ""
        txtTotalBayar.Text = ""
        txtBayar.Text = ""
        txtKembalian.Text = ""
        CboNama_Pegawai.Text = ""
        txtNama_Dokter.Text = ""
        txtNoBukti.Text = ""
    End Sub
    Private Sub DisplayData()
        Modulekoneksi.bukaDB()
        DA = New MySqlDataAdapter("SELECT * From rinciberobat Where NoBukti='" & txtNoBukti.Text & "'", Koneksi)
        ds = New DataSet
        DA.Fill(ds, "rinciberobat")
        DataGridView1.DataSource = ds.Tables("rinciberobat")
        DataGridView1.ReadOnly = True
    End Sub
    Private Sub CekData()
        bukaDB()
        Try
            Dim cmd As New MySqlCommand("SELECT * From berobat Where NoBukti='" & txtNoBukti.Text & "'", Koneksi)
            Dim dr As MySqlDataReader = cmd.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                txtTglDaftar.Text = dr("TglDaftar")
                CboKode_Pasien.Text = dr("Kode_Pasien")
                txtTotalBayar.Text = dr("Total_Bayar")
                CboNama_Pegawai.Text = dr("Nama_Pegawai")
                txtNama_Dokter.Text = dr("Nama_dokter")
                txtNoBukti.Focus()
            Else
                BersihData()
            End If
            dr.Close()
            DisplayData()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
            If Koneksi.State = ConnectionState.Open Then
                Koneksi.Close()

            End If

        End Try
        bukaDB()
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtNoBukti_TextChanged(sender As Object, e As EventArgs) Handles txtNoBukti.TextChanged

    End Sub
End Class