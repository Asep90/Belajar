
Imports MySql.Data.MySqlClient
Public Class FormPasien
    Public strSql As String
    Public DB As MySqlConnection
    Public CMD As MySqlCommand
    Public ADP As MySqlDataAdapter
    Public DR As MySqlDataReader
    Public DS As New DataSet
    Public Simpan, Hapus As String
    Sub Tambah()
        DataGridView1.Enabled = False
        txtKode_Pasien.Clear()
        txtNama_Pasien.Clear()
        txtKelamin.Clear()
        txtAlamat_Pasien.Clear()
        txtTglLahir.Clear()
        txtNoKTP.Clear()
        txtNoHP.Clear()
        txtKode_Pasien.Enabled = True
        txtNama_Pasien.Enabled = True
        txtKelamin.Enabled = True
        txtAlamat_Pasien.Enabled = True
        txtTglLahir.Enabled = True
        txtNoKTP.Enabled = True
        txtNoHP.Enabled = True
        txtKode_Pasien.Focus()
        btnTambah.Enabled = False
        btnHapus.Enabled = False
        btnUbah.Enabled = True
        btnSimpan.Enabled = True
        btnBatal.Enabled = True

    End Sub
    Sub Ubah()
        txtKode_Pasien.Enabled = False
        txtNama_Pasien.Enabled = True
        txtKelamin.Enabled = True
        txtAlamat_Pasien.Enabled = True
        txtTglLahir.Enabled = True
        txtNoKTP.Enabled = True
        txtNoHP.Enabled = True
        txtKode_Pasien.Focus()
        btnTambah.Enabled = True
        btnHapus.Enabled = True
        btnUbah.Enabled = True
        btnSimpan.Enabled = False
        btnBatal.Enabled = True
    End Sub
    Sub Batal()
        DataGridView1.Enabled = True
        txtKode_Pasien.Clear()
        txtNama_Pasien.Clear()
        txtKelamin.Clear()
        txtAlamat_Pasien.Clear()
        txtTglLahir.Clear()
        txtNoKTP.Clear()
        txtNoHP.Clear()
        txtKode_Pasien.Enabled = False
        txtNama_Pasien.Enabled = False
        txtKelamin.Enabled = False
        txtAlamat_Pasien.Enabled = False
        txtTglLahir.Enabled = False
        txtNoKTP.Enabled = False
        txtNoHP.Enabled = False
        btnTambah.Enabled = True
        btnHapus.Enabled = True
        btnUbah.Enabled = True
        btnSimpan.Enabled = False
        btnBatal.Enabled = False
    End Sub
    Sub Bersih()
        txtKode_Pasien.Clear()
        txtNama_Pasien.Clear()
        txtKelamin.Clear()
        txtAlamat_Pasien.Clear()
        txtTglLahir.Clear()
        txtNoKTP.Clear()
        txtNoHP.Clear()
        txtKode_Pasien.Focus()
    End Sub
    Sub IsiGrid()
        Modulekoneksi.bukaDB()
        DA = New MySqlDataAdapter("SELECT * FROM Pasien", Koneksi)
        DS = New DataSet
        DA.Fill(DS, "pasien")
        DataGridView1.DataSource = DS.Tables("pasien")
        DataGridView1.ReadOnly = True
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Call bukaDB()
        CMD = New MySqlCommand("SELECT kd_Pasien from Pasien Where kd_pasien ='" & txtKode_Pasien.Text & "'", Koneksi)
        RD = CMD.ExecuteReader
        RD.Read()
        If btnTambah.Enabled = False Then
            If RD.HasRows Then
                MsgBox("Maaf, Data dengan Nama Kode_Pasien tersebut telah ada", MsgBoxStyle.Exclamation, "Peringatan")
            Else
                If MessageBox.Show("Simpan data baru? ", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Call bukaDB()
                    Simpan = "INSERT INTO Pasien (kd_pasien,Nama_Pasien,Kelamin,Alamat_Pasien,TglLahir,NoKTP,NoHP) VALUES ('" & txtKode_Pasien.Text & "','" & txtNama_Pasien.Text & "','" & txtKelamin.Text & "','" & txtAlamat_Pasien.Text & "','" & txtTglLahir.Text & "','" & txtNoKTP.Text & "','" & txtNoHP.Text & "')"
                    CMD = New MySqlCommand(Simpan, Koneksi)
                    CMD.ExecuteNonQuery()
                    Call IsiGrid()
                    Call Batal()
                    txtKode_Pasien.Focus()
                    MsgBox("Data Tersimpan", MsgBoxStyle.Information, "Information")
                    RD.Close()
                End If
            End If
        End If
    End Sub

   
    
    Private Sub btnTambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTambah.Click
        Call Tambah()
    End Sub

    Private Sub btnUbah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUbah.Click
        bukaDB()
        Call Ubah()
        Try
            Using conn As New MySqlConnection(Koneksi.ConnectionString)
                conn.Open()
                Dim Command As New MySqlCommand("update pasien set Kode_Pasien=@kode_Pasien,Nama_Pasien=@Nama_Pasien,Kelamin=@Kelamin,Alamat_Pasien=@Alamat_Pasien,TglLahir=@TglLahir,NoKTP=@NoKTP,NoHP=@NoHP where Kode_Pasien=@Kode_Pasien", conn)

                With Command.Parameters
                    .AddWithValue("@kode_Pasien", txtKode_Pasien.Text)
                    .AddWithValue("@Nama_Pasien", txtNama_Pasien.Text)
                    .AddWithValue("@kelamin", txtKelamin.Text)
                    .AddWithValue("@Alamat_Pasien", txtAlamat_Pasien.Text)
                    .AddWithValue("@TglLahir", txtTglLahir.Text)
                    .AddWithValue("@NoKTP", txtNoKTP.Text)
                    .AddWithValue("@NoHP", txtNoHP.Text)
                End With
                Command.ExecuteNonQuery()
                MessageBox.Show("Anda Akan Merubah data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "kesalahan Dalam pengimputan Data", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Call IsiGrid()
        Call Bersih()
    End Sub

    Private Sub btnHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHapus.Click
        If txtKode_Pasien.Text = "" Then
            MessageBox.Show("Pilih Dahulu Data Yang Akan diHapus pada tabel diatas!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            If txtKode_Pasien.Text <> "" Then
                If MessageBox.Show("Apakah Anda Yakin akan Menghahapus data tabel diatas!", "konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Call bukaDB()
                    Hapus = "DELETE From Pasien where kd_Pasien='" & txtKode_Pasien.Text & "'"
                    CMD = New MySqlCommand(Hapus, Koneksi)
                    CMD.ExecuteNonQuery()
                    Call IsiGrid()
                    Call Bersih()
                    txtKode_Pasien.Focus()
                    MessageBox.Show("Data Berhasil dihapus", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

                End If
            End If
        End If
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        Call Batal()
    End Sub

    Private Sub btnKeluar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKeluar.Click
        Me.Hide()
        FormUtama.Show()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Call bukaDB()
        CMD = New MySqlCommand("SELECT Kode _Pasien,Nama_Pasien,Kelamin,Alamat_Pasien,TglLahir,NoKtP,NoHP FROM WHERE Kode_Pasien='" & txtKode_Pasien.Text & "'", DB)
        Try
            Dim gridbaris As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            txtKode_Pasien.Text = gridbaris.Cells(0).Value.ToString
            txtNama_Pasien.Text = gridbaris.Cells(1).Value.ToString
            txtKelamin.Text = gridbaris.Cells(2).Value.ToString
            txtAlamat_Pasien.Text = gridbaris.Cells(3).Value.ToString
            txtTglLahir.Text = gridbaris.Cells(4).Value.ToString
            txtNoKTP.Text = gridbaris.Cells(5).Value.ToString
            txtNoHP.Text = gridbaris.Cells(6).Value.ToString
        Catch ex As Exception
            MsgBox("Pilih yang didalam Tabel", "MsgBoxStyle.Information, INFORMASI")

        End Try
    End Sub

    Private Sub FormPasien_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call IsiGrid()
        Call Ubah()
    End Sub
End Class
