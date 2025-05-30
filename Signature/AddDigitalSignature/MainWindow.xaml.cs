using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;
using Syncfusion.Licensing.security;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Security;

namespace AddDigitalSignature
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PdfLoadedDocument loadedDocument;
        const string DatetimeFormat = "dd/MM/yyyy HH:mm:sszzz";
        RectangleF Bounds = RectangleF.Empty;
        static string certFilePath = "MyTestCert.pfx";
        static string certPassword = "YourPassword";
        double pixelsPerDip;
        string filepath;
        string fileSavePath;
        public MainWindow()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NHaF5cXmpCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdgWH9edHVWQmhdUEF2WEA=");
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
#if NETCOREAPP
            filepath = @"../../../Data/Barcode.pdf";
#else
            filepath = @"../../Data/Barcode.pdf";
#endif
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(filepath);
            docView.Load(loadedDocument);
        }
        private void Draw_Click(object sender, RoutedEventArgs e)
        {
            loadedDocument = docView.LoadedDocument;
            docView.AnnotationMode = Syncfusion.Windows.PdfViewer.PdfDocumentView.PdfViewerAnnotationMode.Rectangle;           
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (loadedDocument != null)
            {
                PdfSignature signature = null;
                PdfUnitConvertor unitConvertor = null;

                System.Windows.Point position = new System.Windows.Point(10, 10);

                PdfGraphics graphics = null;
                CreateOwnDigitalID();

                X509Certificate2 x509Cert = new X509Certificate2(certFilePath, certPassword);
                PdfCertificate cert = new PdfCertificate(x509Cert);

                //Create a signature with loaded digital ID.
                signature = new PdfSignature(loadedDocument, loadedDocument.Pages[0], cert, "Signature");

                signature.SignedName = cert.SubjectName;
                signature.Reason = "Testing data";
                signature.LocationInfo = "India";
                signature.EnableValidationAppearance = false;
                signature.EnableLtv = true;

                signature.Settings.DigestAlgorithm = DigestAlgorithm.SHA256;
                signature.Settings.CryptographicStandard = CryptographicStandard.CMS;

                //Create a font to draw text.
                PdfStandardFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 10);
                PdfStandardFont fontMain = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);

                string str = cert.SubjectName;
                var dpi = VisualTreeHelper.GetDpi(this);
                pixelsPerDip = dpi.PixelsPerDip;


                System.Windows.Media.FormattedText formattedTextMain = new System.Windows.Media.FormattedText(str,
                                                System.Globalization.CultureInfo.GetCultureInfo("en-us"),
                                                System.Windows.FlowDirection.LeftToRight,
                                                new System.Windows.Media.Typeface("Times New Roman"), FontSize = 14, System.Windows.Media.Brushes.Black,pixelsPerDip);

                double textWidthMain = formattedTextMain.Width;

                float width = 0;

                if (str.Contains(" "))
                {
                    width = GetWidth(str) + 20;
                }
                else
                {
                    width = (float)textWidthMain + 20;
                }

                double InnerRectWidth = GetInnerRectWidth(str, cert.IssuerName, signature.Reason, signature.LocationInfo, DateTime.Now.ToString(DatetimeFormat));

                string updated = string.Empty;

                bool bcorrectLength = true;


                if (str.Length < 40)
                {
                    string phrase = str;
                    string[] words = phrase.Split(' ');

                    if (words.Count() > 1)
                    {
                        foreach (var word in words)
                        {
                            if (word.Length > 12)
                            {
                                bcorrectLength = false;
                                break;
                            }
                        }

                        if (bcorrectLength)
                            updated = string.Join(Environment.NewLine, words);
                        else
                        {
                            int size = str.Length / 3;
                            IEnumerable<string> s = str.Split(size);
                            updated = string.Join(Environment.NewLine, s);
                        }
                    }
                    else
                    {
                        updated = words[0];
                    }
                }
                else
                {
                    int size = str.Length / 3;

                    IEnumerable<string> s = str.Split(size);
                    updated = string.Join(Environment.NewLine, s);
                }

                unitConvertor = new PdfUnitConvertor();
                PointF bounds = new PointF(((float)position.X / (float)100) * 100, ((float)position.Y / (float)100) * 100);
                PointF point = unitConvertor.ConvertFromPixels(bounds, PdfGraphicsUnit.Point);

                SizeF pageSize = loadedDocument.Pages[0].Size;

                if (point.X + width + InnerRectWidth + 20 > pageSize.Width)
                {
                    point.X = point.X - (point.X + width + (float)InnerRectWidth + 20 - pageSize.Width);
                }
                if (point.Y + 120 > pageSize.Height)
                {
                    point.Y = point.Y - (point.Y + 120 - pageSize.Height);
                }

                //Set bounds to the signature.
                signature.Bounds = GetRelativeBounds(loadedDocument.Pages[0] as PdfLoadedPage, new System.Drawing.RectangleF((float)point.X, (float)point.Y, (float)(width + InnerRectWidth + 20), 120));

                graphics = signature.Appearance.Normal.Graphics;
                PdfPageRotateAngle angle = loadedDocument.Pages[0].Rotation;

                graphics.Save();

                if (angle == PdfPageRotateAngle.RotateAngle90)
                {
                    graphics.TranslateTransform(0, signature.Bounds.Height);
                    graphics.RotateTransform(-90);

                }
                else if (angle == PdfPageRotateAngle.RotateAngle180)
                {
                    graphics.TranslateTransform(signature.Bounds.Width, signature.Bounds.Height);
                    graphics.RotateTransform(-180);

                }
                else if (angle == PdfPageRotateAngle.RotateAngle270)
                {
                    graphics.TranslateTransform(signature.Bounds.Width, 0);
                    graphics.RotateTransform(-270);

                }



                signature.Appearance.Normal.Graphics.DrawRectangle(PdfPens.Black, PdfBrushes.White, new System.Drawing.RectangleF(0, 0, (float)(width + InnerRectWidth + 20), 120));
                signature.Appearance.Normal.Graphics.DrawString(updated, fontMain, PdfBrushes.Black, 10, 0);

                signature.Appearance.Normal.Graphics.DrawString("Digitally Signed by", font, PdfBrushes.Black, width + 10, 0);
                signature.Appearance.Normal.Graphics.DrawString(str, font, PdfBrushes.Black, width + 10, 15);
                signature.Appearance.Normal.Graphics.DrawString("Issued by", font, PdfBrushes.Black, width + 10, 30);
                signature.Appearance.Normal.Graphics.DrawString(cert.IssuerName, font, PdfBrushes.Black, width + 10, 45);
                signature.Appearance.Normal.Graphics.DrawString("Location: " + signature.LocationInfo, font, PdfBrushes.Black, width + 10, 60);

                if (signature.Reason != "Not Available")
                {
                    signature.Appearance.Normal.Graphics.DrawString("Reason: " + signature.Reason, font, PdfBrushes.Black, width + 10, 75);
                    signature.Appearance.Normal.Graphics.DrawString("Signed Date:", font, PdfBrushes.Black, width + 10, 90);
                    signature.Appearance.Normal.Graphics.DrawString(DateTime.Now.ToString(DatetimeFormat), font, PdfBrushes.Black, width + 10, 105);
                }
                else
                {
                    signature.Appearance.Normal.Graphics.DrawString("Signed Date:", font, PdfBrushes.Black, width + 10, 75);
                    signature.Appearance.Normal.Graphics.DrawString(DateTime.Now.ToString(DatetimeFormat), font, PdfBrushes.Black, width + 10, 90);
                }
                Bounds = unitConvertor.ConvertFromPixels(Bounds, PdfGraphicsUnit.Point);
                signature.Bounds = new RectangleF(Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height);
                graphics.Restore();

#if NETCOREAPP
                fileSavePath = @"../../../Barcode_Sign1.pdf";
#else
                fileSavePath = @"../../Barcode_Sign1.pdf";
#endif
                loadedDocument.Save(fileSavePath);

                if (docView.IsLoaded)
                    docView.Unload();

                docView.Load(fileSavePath);
            }
        }

        private static RectangleF GetRelativeBounds(PdfLoadedPage page, RectangleF bounds)
        {
            SizeF pagesize = page.Size;
            RectangleF rectangle = bounds;

            if (page.Rotation == PdfPageRotateAngle.RotateAngle90)
            {
                rectangle.X = bounds.Y;
                rectangle.Y = pagesize.Height - ((bounds.X + bounds.Width));
                rectangle.Width = bounds.Height;
                rectangle.Height = bounds.Width;
            }
            else if (page.Rotation == PdfPageRotateAngle.RotateAngle270)
            {
                rectangle.Y = bounds.X;
                rectangle.X = pagesize.Width - (bounds.Y + bounds.Height);
                rectangle.Width = bounds.Height;
                rectangle.Height = bounds.Width;
            }
            else if (page.Rotation == PdfPageRotateAngle.RotateAngle180)
            {
                rectangle.X = pagesize.Width - (bounds.X + bounds.Width);
                rectangle.Y = pagesize.Height - (bounds.Y + bounds.Height);
            }
            return rectangle;
        }

        private float GetInnerRectWidth(string text, string issuedBy, string Reason, string Location, string Time)
        {
            
            float ret = 0;

            List<Double> lstWidth = new List<double>();
            System.Windows.Media.FormattedText formattedText = new System.Windows.Media.FormattedText(text,
                                                System.Globalization.CultureInfo.GetCultureInfo("en-us"),
                                                System.Windows.FlowDirection.LeftToRight,
                                                new System.Windows.Media.Typeface("Times New Roman"), FontSize = 10, System.Windows.Media.Brushes.Black,pixelsPerDip);

            lstWidth.Add(formattedText.Width);

            System.Windows.Media.FormattedText IssuerText = new System.Windows.Media.FormattedText(issuedBy,
                                               System.Globalization.CultureInfo.GetCultureInfo("en-us"),
                                               System.Windows.FlowDirection.LeftToRight,
                                               new System.Windows.Media.Typeface("Times New Roman"), FontSize = 10, System.Windows.Media.Brushes.Black,pixelsPerDip);

            lstWidth.Add(IssuerText.Width);


            System.Windows.Media.FormattedText reasonText = new System.Windows.Media.FormattedText(Reason,
                                               System.Globalization.CultureInfo.GetCultureInfo("en-us"),
                                               System.Windows.FlowDirection.LeftToRight,
                                               new System.Windows.Media.Typeface("Times New Roman"), FontSize = 10, System.Windows.Media.Brushes.Black, pixelsPerDip);

            lstWidth.Add(reasonText.Width);

            System.Windows.Media.FormattedText locationText = new System.Windows.Media.FormattedText(Location,
                                               System.Globalization.CultureInfo.GetCultureInfo("en-us"),
                                               System.Windows.FlowDirection.LeftToRight,
                                               new System.Windows.Media.Typeface("Times New Roman"), FontSize = 10, System.Windows.Media.Brushes.Black, pixelsPerDip);

            lstWidth.Add(locationText.Width);

            System.Windows.Media.FormattedText timeText = new System.Windows.Media.FormattedText(Time,
                                               System.Globalization.CultureInfo.GetCultureInfo("en-us"),
                                               System.Windows.FlowDirection.LeftToRight,
                                               new System.Windows.Media.Typeface("Times New Roman"), FontSize = 10, System.Windows.Media.Brushes.Black, pixelsPerDip);

            lstWidth.Add(timeText.Width);


            ret = (float)lstWidth.Max();
            return ret;
        }

        private float GetWidth(string str)
        {
            float ret = 0;

            List<Double> lstWidth = new List<double>();
            string phrase = str;
            string[] words = phrase.Split(' ');

            foreach (var word in words)
            {
                FormattedText splitedWord = new FormattedText(word,
                                               System.Globalization.CultureInfo.GetCultureInfo("en-us"),
                                               FlowDirection.LeftToRight,
                                               new Typeface("Times New Roman"), FontSize = 14, System.Windows.Media.Brushes.Black,pixelsPerDip);

                lstWidth.Add(splitedWord.Width);
            }

            ret = (float)lstWidth.Max();
            return ret;
        }

        static void CreateOwnDigitalID()
        {
            // Generate an RSA key pair
            RsaKeyPairGenerator rsaKeyPairGenerator = new RsaKeyPairGenerator();
            rsaKeyPairGenerator.Init(new KeyGenerationParameters(new Org.BouncyCastle.Security.SecureRandom(), 2048));
            AsymmetricCipherKeyPair keyPair = rsaKeyPairGenerator.GenerateKeyPair();

            // Create a self-signed certificate generator
            X509V3CertificateGenerator certificateGenerator = new X509V3CertificateGenerator();
            certificateGenerator.SetSerialNumber(BigInteger.ValueOf(1));
            certificateGenerator.SetSubjectDN(new X509Name("CN=MyTestCert"));
            certificateGenerator.SetIssuerDN(new X509Name("CN=MyTestCert"));
            certificateGenerator.SetNotBefore(DateTime.UtcNow.Date);
            certificateGenerator.SetNotAfter(DateTime.UtcNow.Date.AddYears(1));
            certificateGenerator.SetPublicKey(keyPair.Public);

            // Sign the certificate with the private key
            ISignatureFactory signatureFactory = new Asn1SignatureFactory("SHA256WithRSA", keyPair.Private);
            Org.BouncyCastle.X509.X509Certificate generatedCertificate = certificateGenerator.Generate(signatureFactory);

            // Create a PKCS#12 store to hold the certificate and private key
            Pkcs12Store store = new Pkcs12StoreBuilder().Build();
            X509CertificateEntry certificateEntry = new X509CertificateEntry(generatedCertificate);
            AsymmetricKeyEntry privateKeyEntry = new AsymmetricKeyEntry(keyPair.Private);
            store.SetKeyEntry("alias", privateKeyEntry, new[] { certificateEntry });

            // Export the PKCS#12 store to a byte array
            MemoryStream pfxStream = new MemoryStream();
            store.Save(pfxStream, "YourPassword".ToCharArray(), new Org.BouncyCastle.Security.SecureRandom());

            // Save the byte array to a file            
            File.WriteAllBytes(certFilePath, pfxStream.ToArray());

            Console.WriteLine("Certificate created successfully and saved to " + certFilePath);
        }

        private void docView_ShapeAnnotationChanged(object sender, Syncfusion.Windows.PdfViewer.ShapeAnnotationChangedEventArgs e)
        {
            if (e.Action == Syncfusion.Windows.PdfViewer.AnnotationChangedAction.Add || e.Action == Syncfusion.Windows.PdfViewer.AnnotationChangedAction.Resize)
            {
                Bounds = e.NewBounds;
            }
        }

    }
}
