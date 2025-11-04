"""
PDF to Word Converter
Developed by: Khomotso Jonathan Gole
Contact: 0783672051 / 0795886717
Email: khomotsojonathan2@gmail.com
LinkedIn: https://www.linkedin.com/in/khomotso-jonathan-gole
GitHub: https://github.com/Khomotso-Jonathan-Gole

Copyright (c) 2025 Khomotso Jonathan Gole. All rights reserved.
Licensed under the MIT License.
"""


import sys
import os
from pdf2docx import Converter
import json

def convert_pdf_to_word(pdf_path, docx_path):
    """Convert PDF to Word document"""
    try:

        cv = Converter(pdf_path)
        cv.convert(docx_path, start=0, end=None)
        cv.close()

        return {"success": True, "output_path": docx_path, "message": "Conversion successful"}
    except Exception as e:
        return {"success": False, "error": str(e)}

if __name__ == "__main__":

    if len(sys.argv) != 3:
        print("Usage: python extraction.py <input_pdf> <output_docx>")
        sys.exit(1)

    pdf_path = sys.argv[1]
    docx_path = sys.argv[2]

    result = convert_pdf_to_word(pdf_path, docx_path)
    print(json.dumps(result))

