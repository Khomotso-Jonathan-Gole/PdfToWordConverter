# 📄 PDF to Word Converter

A professional desktop application that converts PDF files to editable Word documents with high accuracy and formatting preservation.

![PDF to Word Converter](https://img.shields.io/badge/PDF-Word_Converter-blue)
![.NET](https://img.shields.io/badge/.NET-6.0-purple)
![Python](https://img.shields.io/badge/Python-3.8%2B-yellow)
![License](https://img.shields.io/badge/License-MIT-green)

## ✨ Features

- **High-Quality Conversion** - Preserves text formatting, images, and layout
- **User-Friendly Interface** - Modern WPF desktop application
- **Batch Processing Ready** - Convert multiple PDFs efficiently
- **OCR Support** - Handles scanned PDF documents
- **Cross-Platform Python Backend** - Robust conversion engine
- **Professional UI** - Beautiful blue-red themed interface
- **Real-time Progress** - Visual feedback during conversion

## 🏗️ Architecture
PDF to Word Converter/
├── 🖥️ CSharpApp/ # WPF Desktop Application (.NET 6.0)
├── 🐍 PythonScripts/ # PDF Processing Backend
├── 📁 Data/ # Input/Output Directory
└── 📊 Reports/ # Conversion Reports


### Technology Stack
- **Frontend**: WPF (Windows Presentation Foundation)
- **Backend**: Python with pdf2docx, PyMuPDF
- **Integration**: Python.NET Runtime
- **UI**: XAML with modern styling

## 🚀 Installation

### Prerequisites
- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Python 3.8+](https://www.python.org/downloads/)
- Windows 10/11

### Setup Steps

1. **Clone the Repository**
   ```bash
   git clone https://github.com/Khomotso-Jonathan-Gole/PdfToWordConverter.git
   cd PdfToWordConverter

cd PythonScripts
pip install -r requirements.txt

cd CSharpApp
dotnet build
dotnet run

📖 Usage
Launch the Application

Run dotnet run from the CSharpApp directory

Or execute the built executable

Convert PDF to Word

Click "Browse PDF" to select your file

Click "Convert to Word Document"

Wait for conversion completion

Find the output .docx file in the same directory

Output

Word document with preserved formatting

Images and tables maintained

Editable text content

🛠️ Development
Project Structure
text
CSharpApp/
├── MainWindow.xaml      # Main application UI
├── MainWindow.xaml.cs   # UI logic and event handlers
├── PythonService.cs     # Python integration service
└── PdfToWordConverter.csproj

PythonScripts/
├── extraction.py        # Main conversion logic
├── validate_extraction.py # Quality validation
├── schema.json         # Data validation schema
└── requirements.txt    # Python dependencies

👨‍💻 Developer
Khomotso Jonathan Gole

📧 Email: khomotsojonathan2@gmail.com

📞 Phone: 0783672051 / 0795886717

💼 LinkedIn: Khomotso Jonathan Gole

💻 GitHub: Khomotso-Jonathan-Gole

🙏 Acknowledgments
pdf2docx library team for excellent PDF conversion capabilities

Python.NET for seamless C#-Python integration

.NET team for robust desktop application framework

📄 License
This project is licensed under the MIT License - see the LICENSE file for details.
