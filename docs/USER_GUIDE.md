# User Guide

## Table of Contents
1. [Getting Started](#getting-started)
2. [Creating Projects](#creating-projects)
3. [Using Quality Tools](#using-quality-tools)
4. [Exporting and Sharing](#exporting-and-sharing)
5. [Tips and Best Practices](#tips-and-best-practices)
6. [Troubleshooting](#troubleshooting)

## Getting Started

### First Launch

When you launch the Kaizen Blitz Application for the first time:
1. The application creates a database file in the application directory
2. A sample project may be created for demonstration purposes
3. You'll see the Project Dashboard as the main screen

### Dashboard Overview

The dashboard displays all your Kaizen projects with:
- Project name and description
- Progress bar showing completion percentage
- Current status (In Progress, Completed, etc.)
- Last modified date
- Quick action buttons (Open, Delete)

## Creating Projects

### Step 1: Start the Wizard
Click the **"NEW PROJECT"** button on the dashboard.

### Step 2: Enter Project Details

**Required Fields:**
- **Project Name**: A clear, descriptive name for your improvement project
  - Example: "Reduce Production Line Downtime"

**Optional Fields:**
- **Description**: Detailed explanation of what you're trying to improve
  - Example: "Analyzing and reducing unplanned downtime on Assembly Line 3"
  
- **Target Area**: Department or area of focus
  - Example: "Manufacturing Floor - Line 3"
  
- **Start Date**: When the project begins (defaults to today)
  
- **Expected Completion Date**: Target date for completion
  - Tip: Most Kaizen Blitz events are 3-5 days
  
- **Team Members**: List of people involved (comma-separated)
  - Example: "John Doe, Jane Smith, Bob Johnson"

### Step 3: Create
Click **"CREATE"** to save your project. It will appear on the dashboard.

## Using Quality Tools

### Five Whys Analysis

**Purpose**: Identify the root cause of a problem by asking "why" repeatedly.

**How to Use:**

1. **Open the Tool**
   - Select your project from the dashboard
   - Navigate to Analysis Phase → Five Whys

2. **Enter Problem Statement**
   - Be specific and measurable
   - Example: "Production line stopped for 2 hours on Monday"

3. **Answer Each "Why"**
   - **Why 1**: "Why did this problem occur?"
     - Example: "The main motor overheated"
   
   - **Why 2**: "Why did the motor overheat?"
     - Example: "The cooling system wasn't working"
   
   - **Why 3**: "Why wasn't the cooling system working?"
     - Example: "The filter was clogged with debris"
   
   - **Why 4**: "Why was the filter clogged?"
     - Example: "The filter hadn't been changed in 6 months"
   
   - **Why 5**: "Why hadn't it been changed?"
     - Example: "There's no preventive maintenance schedule"

4. **Identify Root Cause**
   - Based on your "whys", state the fundamental cause
   - Example: "Lack of preventive maintenance system"

5. **Additional Whys (Optional)**
   - Click **"ADD ANOTHER WHY"** if you need to dig deeper
   - Some problems require 6-7 "whys"

6. **Mark Complete**
   - Check the "Analysis Completed" box
   - Click **"MARK COMPLETE"**

7. **Export**
   - Click **"EXPORT PDF"** to save your analysis
   - Share with team or management

### Ishikawa (Fishbone) Diagram

**Purpose**: Organize potential causes into categories for systematic analysis.

**How to Use:**

1. **Open the Tool**
   - Navigate to Analysis Phase → Ishikawa Diagram

2. **Enter Problem Statement**
   - Same as your Five Whys problem
   - Example: "High defect rate in final assembly"

3. **Add Causes to Each Category**

   The 6M categories:
   
   - **People (Manpower)**
     - Skills, training, fatigue, communication
     - Example: "Inadequate training on new equipment"
   
   - **Process (Methods)**
     - Procedures, workflow, standards
     - Example: "No standard work instructions"
   
   - **Materials**
     - Raw materials, supplies, quality
     - Example: "Inconsistent supplier quality"
   
   - **Equipment (Machines)**
     - Tools, technology, maintenance
     - Example: "Aging equipment needs replacement"
   
   - **Environment**
     - Workspace conditions, temperature, lighting
     - Example: "Poor lighting in inspection area"
   
   - **Management**
     - Leadership, planning, policies
     - Example: "No quality metrics tracked"

4. **Add Causes**
   - Type a cause in the text box under each category
   - Click **"Add Cause"**
   - Add multiple causes per category

5. **Review**
   - Look for patterns across categories
   - Identify which causes appear most significant

6. **Complete and Export**
   - Mark as complete when finished
   - Export to PDF for documentation

### Action Plan

**Purpose**: Create a detailed plan to implement improvements.

**How to Use:**

1. **Open the Tool**
   - Navigate to Implementation Phase → Action Plan

2. **Add Tasks**
   - Click **"ADD TASK"**
   - A new row appears in the grid

3. **Fill in Task Details**

   **For Each Task:**
   - **Task Description**: What needs to be done
     - Example: "Install new filter monitoring system"
   
   - **Responsible Person**: Who will do it
     - Example: "John Doe"
   
   - **Deadline**: When it should be completed
     - Click the date picker to select
   
   - **Status**: Current progress
     - Not Started
     - In Progress
     - Completed
     - Blocked
     - Cancelled
   
   - **Priority**: Importance level
     - High
     - Medium
     - Low
   
   - **Notes**: Additional details or dependencies
     - Example: "Requires budget approval first"

4. **Update Progress**
   - As work progresses, update the Status column
   - When a task is done, mark it "Completed"
   - The system automatically records completion date

5. **Filter and Sort**
   - Use the filter dropdown to show only certain statuses
   - Click column headers to sort

6. **Export to Excel**
   - Click **"EXPORT EXCEL"**
   - Opens formatted spreadsheet with all tasks
   - Share with team for tracking

7. **Mark Plan Complete**
   - When all critical tasks are done
   - Click **"MARK COMPLETE"**

## Exporting and Sharing

### Exporting Individual Tools

1. Open the tool view (Five Whys, Ishikawa, Action Plan)
2. Click the **"EXPORT PDF"** or **"EXPORT EXCEL"** button
3. Choose save location
4. File is generated with current date in filename

### Generating Project Summary

1. Complete all required tools in your project
2. Navigate to the project overview
3. Click **"Generate Summary Report"**
4. A comprehensive PDF is created with:
   - Cover page
   - Project details
   - All completed tool outputs
   - Charts and diagrams
   - Team member list

### Emailing Reports

1. Generate the PDF report
2. Click **"Email Report"**
3. Your default email client opens
4. The PDF is saved to a temp location
5. Manually attach the file (noted in the prompt)
6. Send to stakeholders

### Creating ZIP Archive

1. Select a completed project
2. Click **"Download Archive"**
3. A ZIP file is created containing:
   - Project summary PDF
   - Individual tool PDFs
   - Excel exports
   - All organized in folders

## Tips and Best Practices

### Project Management
- **Start Small**: Begin with a manageable scope
- **Set Clear Goals**: Define what success looks like
- **Involve the Right People**: Include those who do the work daily
- **Document Everything**: Use the tools to capture decisions

### Using Five Whys
- **Be Specific**: Vague "whys" lead to vague answers
- **Avoid Blame**: Focus on process, not people
- **Verify Each Answer**: Make sure each "why" is factual
- **Stop When You Reach Root Cause**: It might be less than 5 or more than 5

### Using Ishikawa Diagrams
- **Brainstorm First**: Get all ideas out before organizing
- **Use All Categories**: Even if some seem less relevant
- **Combine with Five Whys**: Use both tools together
- **Prioritize**: Not all causes are equal

### Action Planning
- **SMART Goals**: Specific, Measurable, Achievable, Relevant, Time-bound
- **Assign Owners**: Every task needs a responsible person
- **Track Progress**: Update status regularly
- **Address Blockers**: Note and resolve obstacles quickly

### General Tips
- **Save Frequently**: Click SAVE often while working
- **Review Before Completing**: Once marked complete, review carefully
- **Use Exports**: Share progress with stakeholders regularly
- **Iterate**: Kaizen is continuous improvement, revisit projects

## Troubleshooting

### Application Won't Start
- **Solution**: Ensure .NET 6.0 Runtime is installed
- Check: Windows 10 or later required

### Database Errors
- **Solution**: Delete `kaizenblitz.db` file and restart
- Note: This will delete all projects (backup first)

### Export Fails
- **Solution**: Check write permissions on save location
- Try: Save to Documents folder instead

### Can't See Projects
- **Solution**: Check search/filter settings
- Click "All" filter button to show everything

### PDF Export Issues
- **Solution**: Ensure disk space available
- Check: QuestPDF license (Community license should work)

### Excel Export Issues
- **Solution**: Close any open Excel files with same name
- Check: EPPlus library is installed

### Missing Data After Update
- **Solution**: Database migrations should run automatically
- Try: Delete and recreate database (warning: data loss)

### Performance Slow
- **Solution**: Close and restart application
- Check: Large number of projects (>100)

## Keyboard Shortcuts

- **Ctrl + N**: New Project
- **Ctrl + S**: Save current view
- **Ctrl + F**: Search projects
- **F5**: Refresh project list
- **Ctrl + E**: Export current view
- **Alt + F4**: Exit application

## Getting Help

If you need additional assistance:
1. Check this user guide
2. Review the ARCHITECTURE.md for technical details
3. Open an issue on GitHub
4. Contact support team

---

**Happy Improving! Remember: Kaizen means continuous improvement!** 🚀
