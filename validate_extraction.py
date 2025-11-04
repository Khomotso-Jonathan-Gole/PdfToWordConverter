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


# validate_extraction.py
"""
Validate all JSON outputs in data/output_json/ against schema.json.
Usage:
    python validate_extraction.py
"""

import json
from pathlib import Path
from jsonschema import validate, ValidationError

SCHEMA_FILE = Path("schema.json")
DATA_DIR = Path("data/output_json")

def validate_file(schema, data_path):
    data = json.loads(data_path.read_text())
    try:
        validate(instance=data, schema=schema)
        print(f"✅ {data_path.name}: valid")
    except ValidationError as e:
        path = '.'.join(map(str, e.path)) if e.path else '<root>'
        print(f"❌ {data_path.name}: error at {path} → {e.message}")

def main():
    schema = json.loads(SCHEMA_FILE.read_text())
    for file in DATA_DIR.glob("*.json"):
        validate_file(schema, file)

if __name__ == "__main__":
    main()

