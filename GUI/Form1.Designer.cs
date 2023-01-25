using Ubidots;
using System;
using System.Windows.Forms;

namespace MQTT_CSharp;
public partial class Form1{
    TextBox vUmidade;
    TextBox vTemp;
    Button btnEnviar1;
    GroupBox quadroEnviar;
    GroupBox quadroPegar;
    Button btnPegar1;
    Label lbTemp;
    Label lbUmidade;
    Button btnEnviar2;
    Label printTemperatura;
    Label printUmidade2;
    Label printAceleracao;
    Label printVelocidade;

    private void funcTemp(object sender, EventArgs e){
        Dictionary<string,int> data=new Dictionary<string,int>();
        try{
            data["temperatura"]=int.Parse(vTemp.Text);
        }
        catch{
            MessageBox.Show("Valor inserido inválido!!\nSomente números.","ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        commands.PostDATA(data);
        vTemp.Text="";
        funcGET();
    }
    private void funcUmidade(object sender, EventArgs e){
        Dictionary<string,int> data=new Dictionary<string,int>();
        try{
            data["umidade"]=int.Parse(vUmidade.Text);
        }
        catch{
            MessageBox.Show("Valor inserido inválido!!\nSomente números.","ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        commands.PostDATA(data);
        vUmidade.Text="";
        funcGET();
    }
    private void funcPEGAR(object sender, EventArgs e){
        funcGET();
    }
    private async void funcGET(){
        string[] data=await commands.GetDATA();
        Label[] varLabels={printVelocidade,printAceleracao,printUmidade2,printTemperatura};
        string[] labelsBasic={"Velociade: ","Aceleração: ","Umidade: ","Temperatura: "};
        for(int i=0;i<4;i++){
            varLabels[i].Text=labelsBasic[i]+data[i];
        }
    }
    private void InitializeComponent(){
        Text = "Ubidots";
        Size = new System.Drawing.Size(320, 250);

        quadroPegar = new GroupBox();
        btnPegar1 = new Button();
        quadroEnviar = new GroupBox();
        lbTemp = new Label();
        vTemp = new TextBox();
        lbUmidade = new Label();
        printTemperatura = new Label();
        btnEnviar2 = new Button();
        this.btnEnviar1=new Button();
        printAceleracao = new Label();
        printUmidade2 = new Label();
        vUmidade = new TextBox();
        printVelocidade = new Label();


        btnEnviar1.Text = "Enviar";
        btnEnviar1.ForeColor = System.Drawing.Color.Blue;
        btnEnviar1.Location = new System.Drawing.Point(5, 75);
        btnEnviar1.Click += new System.EventHandler(funcTemp);
        quadroEnviar.Controls.Add(btnEnviar1);

        btnEnviar2.Text = "Enviar";
        btnEnviar2.ForeColor = System.Drawing.Color.Blue;
        btnEnviar2.Location = new System.Drawing.Point(5, 170);
        btnEnviar2.Click += new System.EventHandler(funcUmidade);
        quadroEnviar.Controls.Add(btnEnviar2);


        quadroPegar.Text = "Pegar informações";
        quadroPegar.Location = new System.Drawing.Point(160, 0);
        quadroPegar.Size = new System.Drawing.Size(140, 100);
        Controls.Add(quadroPegar);

        
        btnPegar1.Text = "Pegar";
        btnPegar1.ForeColor = System.Drawing.Color.Blue;
        btnPegar1.Location=new System.Drawing.Point(30,30);
        btnPegar1.Click += new System.EventHandler(funcPEGAR);
        quadroPegar.Controls.Add(btnPegar1);

        quadroEnviar.Text = "Enviar informações";
        quadroEnviar.Location = new System.Drawing.Point(5, 0);
        quadroEnviar.Size = new System.Drawing.Size(150, 210);
        Controls.Add(quadroEnviar);

        lbTemp.Text = "Temperatura";
        lbTemp.Location = new System.Drawing.Point(5, 20);
        quadroEnviar.Controls.Add(lbTemp);

        vTemp.Location = new System.Drawing.Point(10, 45);
        quadroEnviar.Controls.Add(vTemp);

        lbUmidade.Text = "Umidade";
        lbUmidade.Location = new System.Drawing.Point(4, 115);
        quadroEnviar.Controls.Add(lbUmidade);

        vUmidade.Location = new System.Drawing.Point(10, 140);
        quadroEnviar.Controls.Add(vUmidade);


        printTemperatura.Text = "Temperatura: ";
        printTemperatura.Location = new Point(160, 105);

        printUmidade2.Text = "Umidade: ";
        printUmidade2.Location = new Point(160, 125);

        printVelocidade.Text = "Velocidade: ";
        printVelocidade.Location = new Point(160, 145);

        printAceleracao.Text = "Aceleração: ";
        printAceleracao.Location = new Point(160, 165);

        // Add all the Labels in the form
        Controls.Add(printTemperatura);
        Controls.Add(printUmidade2);
        Controls.Add(printVelocidade);
        Controls.Add(printAceleracao);
        
    }
}
