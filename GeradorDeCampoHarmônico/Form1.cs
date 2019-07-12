using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net;

namespace GeradorDeCampoHarmônico
{
    public partial class Form1 : Form
    {
        private string[] arrNotesText = new string[12];
        private string[] arrIntervalsText = new string[11];
        private int[] arrIntIntervals = new int[11];
        private int[] arrFormulaEscalaMaior = new int[6];
        private int[] arrSomaEscalaMaior = new int[6];
        private string[,] arrCampoHarmonicoEscalaMaior = new string[7,1];
        private string[] arrNotesTextSite = new string[12];
        private string[,] arrCampoHarmonicoEscalaMaiorSite = new string[7,1];
        private string[,] arrNomesDasImagensFinal = new string[7,12];
        private int[,] arrNotasCalculadasFinal = new int[7, 12];

        public Form1()
        {
            InitializeComponent();            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetValoresGerais();
        }

        #region "CalculaNotasEIntervalos"
        private void SetValoresGerais()
        {
            arrNotesText[0] = "C";
            arrNotesText[1] = "C#";
            arrNotesText[2] = "D";
            arrNotesText[3] = "D#";
            arrNotesText[4] = "E";
            arrNotesText[5] = "F";
            arrNotesText[6] = "F#";
            arrNotesText[7] = "G";
            arrNotesText[8] = "G#";
            arrNotesText[9] = "A";
            arrNotesText[10] = "A#";
            arrNotesText[11] = "B";

            arrIntervalsText[0] = "2b";
            arrIntervalsText[1] = "2";
            arrIntervalsText[2] = "3m";
            arrIntervalsText[3] = "3M";
            arrIntervalsText[4] = "4";
            arrIntervalsText[5] = "5b";
            arrIntervalsText[6] = "5";
            arrIntervalsText[7] = "5#";
            arrIntervalsText[8] = "6";
            arrIntervalsText[9] = "7";
            arrIntervalsText[10] = "7M";

            arrIntIntervals[0] = 1;
            arrIntIntervals[1] = 2;
            arrIntIntervals[2] = 3;
            arrIntIntervals[3] = 4;
            arrIntIntervals[4] = 5;
            arrIntIntervals[5] = 6;
            arrIntIntervals[6] = 7;
            arrIntIntervals[7] = 8;
            arrIntIntervals[8] = 9;
            arrIntIntervals[9] = 10;
            arrIntIntervals[10] = 11;

            arrFormulaEscalaMaior[0] = 1;
            arrFormulaEscalaMaior[1] = 3;
            arrFormulaEscalaMaior[2] = 4;
            arrFormulaEscalaMaior[3] = 6;
            arrFormulaEscalaMaior[4] = 8;
            arrFormulaEscalaMaior[5] = 10;

            arrSomaEscalaMaior[0] = 2;
            arrSomaEscalaMaior[1] = 2;
            arrSomaEscalaMaior[2] = 1;
            arrSomaEscalaMaior[3] = 2;
            arrSomaEscalaMaior[4] = 2;
            arrSomaEscalaMaior[5] = 2;

            arrCampoHarmonicoEscalaMaior[0,0] = "";
            arrCampoHarmonicoEscalaMaior[1,0] = "m";
            arrCampoHarmonicoEscalaMaior[2,0] = "m";
            arrCampoHarmonicoEscalaMaior[3,0] = "";
            arrCampoHarmonicoEscalaMaior[4,0] = "";
            arrCampoHarmonicoEscalaMaior[5,0] = "m";
            arrCampoHarmonicoEscalaMaior[6,0] = "m5b";

            arrNotesTextSite[0] = "c";
            arrNotesTextSite[1] = "d_flat";
            arrNotesTextSite[2] = "d";
            arrNotesTextSite[3] = "e_flat";
            arrNotesTextSite[4] = "e";
            arrNotesTextSite[5] = "f";
            arrNotesTextSite[6] = "g_flat";
            arrNotesTextSite[7] = "g";
            arrNotesTextSite[8] = "a_flat";
            arrNotesTextSite[9] = "a";
            arrNotesTextSite[10] = "b_flat";
            arrNotesTextSite[11] = "b";

            arrCampoHarmonicoEscalaMaiorSite[0, 0] = "";
            arrCampoHarmonicoEscalaMaiorSite[1, 0] = "m";
            arrCampoHarmonicoEscalaMaiorSite[2, 0] = "m";
            arrCampoHarmonicoEscalaMaiorSite[3, 0] = "";
            arrCampoHarmonicoEscalaMaiorSite[4, 0] = "";
            arrCampoHarmonicoEscalaMaiorSite[5, 0] = "m";
            arrCampoHarmonicoEscalaMaiorSite[6, 0] = "dim";
        }

        private void Processa()
        {
            for (int i = 0; i < arrNotesText.Length; i++)
            {
                Calcula(i);
            }

            for (int i = 0; i < arrNomesDasImagensFinal.GetLength(1); i++)
            {
                for (int j = 0; j < arrNomesDasImagensFinal.GetLength(0); j++)
                {
                    BaixaImagens(arrNomesDasImagensFinal[j, i], arrNotesText[i]);
                }
            }

            for (int i = 0; i < arrNomesDasImagensFinal.GetLength(1); i++)
            {
                GeraPdf(i);
            }
        }

        private void Calcula(int intNotaInicial)
        {
            int[] arrNotasCalculadas = new int[7];
            string[] arrNomesDasImagens = new string[7];

            arrNotasCalculadas[0] = intNotaInicial;

            for (int i = 1; i < arrNotasCalculadas.Length; i++)
            {
                arrNotasCalculadas[i] = arrNotasCalculadas[i - 1] + arrSomaEscalaMaior[i - 1];
                if (arrNotasCalculadas[i] > 11)
                {
                    arrNotasCalculadas[i] -= 12;
                }
            }

            for (int i = 0; i < arrNotasCalculadas.Length; i++)
            {                
                if (arrNotesTextSite[arrNotasCalculadas[i]].Contains("_flat"))
                {
                    if (arrCampoHarmonicoEscalaMaiorSite[i,0] == "")
                    {
                        arrNomesDasImagens[i] = arrNotesTextSite[arrNotasCalculadas[i]];
                    } else
                    {
                        arrNomesDasImagens[i] = arrNotesTextSite[arrNotasCalculadas[i]] + "_" + arrCampoHarmonicoEscalaMaiorSite[i, 0];
                    }                    
                } else
                {
                    arrNomesDasImagens[i] = arrNotesTextSite[arrNotasCalculadas[i]] + arrCampoHarmonicoEscalaMaiorSite[i, 0];
                }
                arrNotasCalculadasFinal[i, intNotaInicial] = arrNotasCalculadas[i];
            }

            for (int i = 0; i < arrNomesDasImagens.Length; i++)
            {
                arrNomesDasImagensFinal[i, intNotaInicial] = arrNomesDasImagens[i];
            }            
        }
        #endregion

        #region "BaixaImagens"
        private void BaixaImagens(string imagemParaBaixar, string pastaParaBaixar)
        {
            string pathFolder = @"C:\Users\PauloCezar\Documents\KuryArt\Arte\Música\Estudos\Criados\Teclado\Campos Harmônicos\Teste\" + pastaParaBaixar;
            string fileName = imagemParaBaixar + ".png";
            string uri = "https://www.pianochord.org/images/" + fileName;
            WebClient client = new WebClient();
            CheckFolderExists(pathFolder);
            client.DownloadFile(uri, pathFolder + @"\" + fileName);
            client.Dispose();
        }

        private void CheckFolderExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        #endregion

        #region "GeraPDF"
        private void GeraPdf(int nota)
        {
            Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
            doc.SetMargins(40, 40, 40, 80);//estibulando o espaçamento das margens que queremos
            doc.AddCreationDate();//adicionando as configuracoes

            //caminho onde sera criado o pdf + nome desejado
            //OBS: o nome sempre deve ser terminado com .pdf
            string caminho = @"C:\Users\PauloCezar\Documents\KuryArt\Arte\Música\Estudos\Criados\Teclado\Campos Harmônicos\Teste\" + arrNotesText[nota] + ".pdf";

            //criando o arquivo pdf embranco, passando como parametro a variavel                
            //doc criada acima e a variavel caminho 
            //tambem criada acima.
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));

            doc.Open();

            //criando uma string vazia
            string dados = "";       
            
            //criando a variavel para paragrafo
            Paragraph paragrafo = new Paragraph(0.0f, dados, new iTextSharp.text.Font());
            //etipulando o alinhamneto
            paragrafo.Alignment = Element.ALIGN_CENTER;
            //Alinhamento Justificado
            //adicioando texto
            paragrafo.Add("Campo Harmônico Maior - " + arrNotesText[nota]);
            paragrafo.Font.Size = 24.0f;
            paragrafo.Font.SetStyle("bold");

            //acidionado paragrafo ao documento
            doc.Add(paragrafo);

            //=== ADICIONANDO IMAGENS ===
            float scaleMultiplier = 60f;
            float widthMin = 0.05f;
            float widthMed = 0.285f;
            float widthMax = 0.52f;
            float heightTop = 0.65f;
            float heightMiddleTop = 0.45f;
            float heightMiddleBottom = 0.25f;
            float heightBottom = 0.05f;

            //chord1
            iTextSharp.text.Image chord1 = iTextSharp.text.Image.GetInstance(@"C:\Users\PauloCezar\Documents\KuryArt\Arte\Música\Estudos\Criados\Teclado\Campos Harmônicos\Teste\" + arrNotesText[nota] + @"\" + arrNomesDasImagensFinal[0,nota] + ".png");
            chord1.ScalePercent(scaleMultiplier);
            chord1.SetAbsolutePosition(doc.PageSize.Width * widthMin,
                                           doc.PageSize.Height * heightTop);            
            doc.Add(chord1);
            //chord2
            iTextSharp.text.Image chord2 = iTextSharp.text.Image.GetInstance(@"C:\Users\PauloCezar\Documents\KuryArt\Arte\Música\Estudos\Criados\Teclado\Campos Harmônicos\Teste\" + arrNotesText[nota] + @"\" + arrNomesDasImagensFinal[1, nota] + ".png");
            chord2.ScalePercent(scaleMultiplier);
            chord2.SetAbsolutePosition(doc.PageSize.Width * widthMax,
                                           doc.PageSize.Height * heightTop);
            doc.Add(chord2);
            //chord3
            iTextSharp.text.Image chord3 = iTextSharp.text.Image.GetInstance(@"C:\Users\PauloCezar\Documents\KuryArt\Arte\Música\Estudos\Criados\Teclado\Campos Harmônicos\Teste\" + arrNotesText[nota] + @"\" + arrNomesDasImagensFinal[2, nota] + ".png");
            chord3.ScalePercent(scaleMultiplier);
            chord3.SetAbsolutePosition(doc.PageSize.Width * widthMin,
                                           doc.PageSize.Height * heightMiddleTop);
            doc.Add(chord3);
            //chord4
            iTextSharp.text.Image chord4 = iTextSharp.text.Image.GetInstance(@"C:\Users\PauloCezar\Documents\KuryArt\Arte\Música\Estudos\Criados\Teclado\Campos Harmônicos\Teste\" + arrNotesText[nota] + @"\" + arrNomesDasImagensFinal[3, nota] + ".png");
            chord4.ScalePercent(scaleMultiplier);
            chord4.SetAbsolutePosition(doc.PageSize.Width * widthMax,
                                           doc.PageSize.Height * heightMiddleTop);
            doc.Add(chord4);
            //chord5
            iTextSharp.text.Image chord5 = iTextSharp.text.Image.GetInstance(@"C:\Users\PauloCezar\Documents\KuryArt\Arte\Música\Estudos\Criados\Teclado\Campos Harmônicos\Teste\" + arrNotesText[nota] + @"\" + arrNomesDasImagensFinal[4, nota] + ".png");
            chord5.ScalePercent(scaleMultiplier);
            chord5.SetAbsolutePosition(doc.PageSize.Width * widthMin,
                                           doc.PageSize.Height * heightMiddleBottom);
            doc.Add(chord5);
            //chord6
            iTextSharp.text.Image chord6 = iTextSharp.text.Image.GetInstance(@"C:\Users\PauloCezar\Documents\KuryArt\Arte\Música\Estudos\Criados\Teclado\Campos Harmônicos\Teste\" + arrNotesText[nota] + @"\" + arrNomesDasImagensFinal[5, nota] + ".png");
            chord6.ScalePercent(scaleMultiplier);
            chord6.SetAbsolutePosition(doc.PageSize.Width * widthMax,
                                           doc.PageSize.Height * heightMiddleBottom);
            doc.Add(chord6);
            //chord7
            iTextSharp.text.Image chord7 = iTextSharp.text.Image.GetInstance(@"C:\Users\PauloCezar\Documents\KuryArt\Arte\Música\Estudos\Criados\Teclado\Campos Harmônicos\Teste\" + arrNotesText[nota] + @"\" + arrNomesDasImagensFinal[6, nota] + ".png");
            chord7.ScalePercent(scaleMultiplier);
            chord7.SetAbsolutePosition(doc.PageSize.Width * widthMed,
                                           doc.PageSize.Height * heightBottom);
            doc.Add(chord7);

            //=== ADICIONANDO TEXTO COM OS NOMES DOS ACORDES ===
            //chord1
            string dadosTextoAcorde1 = arrNotesText[arrNotasCalculadasFinal[0,nota]] + arrCampoHarmonicoEscalaMaior[0,0];
            PdfContentByte cb1 = writer.DirectContent;
            float chord1_PosX = chord1.AbsoluteX + ((chord1.Width * (scaleMultiplier / 100)) / 2);
            float chord1_PosY = chord1.AbsoluteY + (chord1.Height * (scaleMultiplier / 100)) + 10f;
            cb1.BeginText();
            cb1.SetFontAndSize(BaseFont.CreateFont(), 16.0f);
            cb1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, dadosTextoAcorde1, chord1_PosX, chord1_PosY, 0f);
            cb1.EndText();
            //chord2
            string dadosTextoAcorde2 = arrNotesText[arrNotasCalculadasFinal[1, nota]] + arrCampoHarmonicoEscalaMaior[1, 0];
            PdfContentByte cb2 = writer.DirectContent;
            float chord2_PosX = chord2.AbsoluteX + ((chord2.Width * (scaleMultiplier / 100)) / 2);
            float chord2_PosY = chord2.AbsoluteY + (chord2.Height * (scaleMultiplier / 100)) + 10f;
            cb2.BeginText();
            cb2.SetFontAndSize(BaseFont.CreateFont(), 16.0f);
            cb2.ShowTextAligned(PdfContentByte.ALIGN_CENTER, dadosTextoAcorde2, chord2_PosX, chord2_PosY, 0f);
            cb2.EndText();
            //chord3
            string dadosTextoAcorde3 = arrNotesText[arrNotasCalculadasFinal[2, nota]] + arrCampoHarmonicoEscalaMaior[2, 0];
            PdfContentByte cb3 = writer.DirectContent;
            float chord3_PosX = chord3.AbsoluteX + ((chord3.Width * (scaleMultiplier / 100)) / 2);
            float chord3_PosY = chord3.AbsoluteY + (chord3.Height * (scaleMultiplier / 100)) + 10f;
            cb3.BeginText();
            cb3.SetFontAndSize(BaseFont.CreateFont(), 16.0f);
            cb3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, dadosTextoAcorde3, chord3_PosX, chord3_PosY, 0f);
            cb3.EndText();
            //chord4
            string dadosTextoAcorde4 = arrNotesText[arrNotasCalculadasFinal[3, nota]] + arrCampoHarmonicoEscalaMaior[3, 0];
            PdfContentByte cb4 = writer.DirectContent;
            float chord4_PosX = chord4.AbsoluteX + ((chord4.Width * (scaleMultiplier / 100)) / 2);
            float chord4_PosY = chord4.AbsoluteY + (chord4.Height * (scaleMultiplier / 100)) + 10f;
            cb4.BeginText();
            cb4.SetFontAndSize(BaseFont.CreateFont(), 16.0f);
            cb4.ShowTextAligned(PdfContentByte.ALIGN_CENTER, dadosTextoAcorde4, chord4_PosX, chord4_PosY, 0f);
            cb4.EndText();
            //chord5
            string dadosTextoAcorde5 = arrNotesText[arrNotasCalculadasFinal[4, nota]] + arrCampoHarmonicoEscalaMaior[4, 0];
            PdfContentByte cb5 = writer.DirectContent;
            float chord5_PosX = chord5.AbsoluteX + ((chord5.Width * (scaleMultiplier / 100)) / 2);
            float chord5_PosY = chord5.AbsoluteY + (chord5.Height * (scaleMultiplier / 100)) + 10f;
            cb5.BeginText();
            cb5.SetFontAndSize(BaseFont.CreateFont(), 16.0f);
            cb5.ShowTextAligned(PdfContentByte.ALIGN_CENTER, dadosTextoAcorde5, chord5_PosX, chord5_PosY, 0f);
            cb5.EndText();
            //chord6
            string dadosTextoAcorde6 = arrNotesText[arrNotasCalculadasFinal[5, nota]] + arrCampoHarmonicoEscalaMaior[5, 0];
            PdfContentByte cb6 = writer.DirectContent;
            float chord6_PosX = chord6.AbsoluteX + ((chord6.Width * (scaleMultiplier / 100)) / 2);
            float chord6_PosY = chord6.AbsoluteY + (chord6.Height * (scaleMultiplier / 100)) + 10f;
            cb6.BeginText();
            cb6.SetFontAndSize(BaseFont.CreateFont(), 16.0f);
            cb6.ShowTextAligned(PdfContentByte.ALIGN_CENTER, dadosTextoAcorde6, chord6_PosX, chord6_PosY, 0f);
            cb6.EndText();
            //chord7
            string dadosTextoAcorde7 = arrNotesText[arrNotasCalculadasFinal[6, nota]] + arrCampoHarmonicoEscalaMaior[6, 0];
            PdfContentByte cb7 = writer.DirectContent;
            float chord7_PosX = chord7.AbsoluteX + ((chord7.Width * (scaleMultiplier / 100)) / 2);
            float chord7_PosY = chord7.AbsoluteY + (chord7.Height * (scaleMultiplier / 100)) + 10f;
            cb7.BeginText();            
            cb7.SetFontAndSize(BaseFont.CreateFont(), 16.0f);              
            cb7.ShowTextAligned(PdfContentByte.ALIGN_CENTER, dadosTextoAcorde7, chord7_PosX, chord7_PosY, 0f);
            cb7.EndText();

            doc.Close();
        }

        private void btnProcessar_Click(object sender, EventArgs e)
        {
            //GeraPdf(0);
            //BaixaImagens("", "");
            Processa();
        }
        #endregion


    }
}
