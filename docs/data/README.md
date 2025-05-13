# Wildfrost Data Reference Format

## Overview
This directory contains reformatted data files for the Wildfrost game project. The files have been structured in a consistent, AI-friendly format to make them more accessible for both human readers and automated systems. All original files have been preserved, with reformatted versions having the suffix `_reformatted`.

## Format Structure
Each data file follows a consistent structure:

1. **YAML Frontmatter**: Metadata about the file at the top
   ```yaml
   ---
   title: [File Title]
   description: [Brief description of the file contents]
   file_type: data_reference
   data_format: json
   updated: 2025-05-12
   source_file: [Original filename]
   ---
   ```

2. **Summary Section**: A concise overview of the file contents and purpose

3. **Data Usage Section**: Guidelines for how to use the data effectively

4. **JSON Data Section**: The core data in structured JSON format
   ```json
   {
     "title": "[Title]",
     "description": "[Description]",
     "data_items": [
       {"property1": "value1", "property2": "value2", ...},
       ...
     ]
   }
   ```

5. **Explanatory Sections**: Additional context, categorizations, and implementation notes

## Benefits of This Format

### For Humans
- Consistent structure across all data files
- Clear section headers for easy navigation
- Explanatory text to provide context and usage guidance
- Proper Markdown formatting for readability

### For AI Systems
- Structured data in JSON format for easy parsing
- Consistent property naming conventions
- Explicit metadata in YAML frontmatter
- Clear delineation between descriptive text and data

## Managing File Formats
This directory contains both original files (e.g., `Keywords.md`) and reformatted versions (e.g., `Keywords_reformatted.md`). To help manage these files, several PowerShell scripts are included:

1. **apply_reformatted_files.ps1**: Replaces original files with their reformatted versions, creating backups of the originals in a `original_files_backup` directory
2. **restore_original_files.ps1**: Restores the original files from backups if you've applied the reformatted versions
3. **rename_reformatted_files.ps1**: Alternative method to replace originals with reformatted versions

To run these scripts:
1. Open PowerShell
2. Navigate to this directory
3. Run the script with `.\script_name.ps1`

## Using the Data
When implementing new game features or modding Wildfrost, you can reference these data files to understand:

- Available game entities (cards, effects, traits, etc.)
- Implementation details and technical properties
- Naming conventions and type systems
- Relationships between different game elements

The JSON format makes it particularly easy to:
- Import the data into development tools
- Parse and process the data programmatically
- Search for specific properties or patterns
- Maintain consistency with existing game elements
