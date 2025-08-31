# Logic App Evaluation Project

This project demonstrates how to integrate an Azure Logic App Standard workflow with a custom .NET assembly for validating customer data (email and phone number). The workflow receives JSON input, validates the fields using a custom DLL, and returns a structured JSON response.

---

## Step 1 – Prerequisites / Installation

Before running this project locally, ensure you have the following tools installed on your machine:

- **.NET 6 SDK** (version 6.0.x)  
  👉 Download: [https://dotnet.microsoft.com/en-us/download/dotnet/6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

- **Azure Functions Core Tools v4**  
  👉 Download MSI (Windows): [https://github.com/Azure/azure-functions-core-tools/releases](https://github.com/Azure/azure-functions-core-tools/releases)

- **Azurite (Azure Storage Emulator)**  
  Install via npm:  
  ```bash
  npm install -g azurite
  ```

- **Visual Studio Code**  
  Install the following extensions in VS Code:
  - *Azure Logic Apps (Standard)*
  - *Azure Functions*

- **Node.js and npm**  
  Required by Logic App Standard tooling.  
  👉 Recommended: Node.js 18 or above.  
  👉 Verify installation:  
  ```bash
  node -v
  npm -v
  ```

- **Postman**  
  Tool for testing the HTTP endpoints of your Logic App.  
  👉 Download: [https://www.postman.com/downloads/](https://www.postman.com/downloads/)

✅ Once all of these are installed, you are ready to start building.

## Step 2 – Customer Validation Project

1. Navigate to the root project directory.
2. Create a new Class Library project for customer validations.
3. Implement validation logic (email & phone number) in this project.  
   *(Code already present in repository under `/CustomerValidation` folder)*.
4. Build the project to generate `CustomerValidation.dll` inside the `bin/Debug/net6.0/` folder.
5. Test the project using the **CustomerValidation.Test** project.  
   Run tests and verify:
   - For valid email/phone → output shows “Email is valid”, “Phone is valid”.
   - For invalid inputs → output shows validation error messages.
6. Confirm all tests passed.


## Step 3 – Logic App Standard Project Setup

1. **Create Logic App Project**  
   - In **Visual Studio Code**, open the Command Palette (`Ctrl+Shift+P` or `Cmd+Shift+P` on Mac).  
   - Select **“Azure Logic Apps: Create New Logic App Project”**.  
   - Provide a project name (e.g., `LogicAppProj`).  
   - Choose **Stateful Workflow** (since we need to persist the state across steps).  
   - Enter a workflow name (e.g., `ValidateCustomerData`).  
   - VS Code generates the **Logic App project structure** with required files like:
     - `workflow.json` (main workflow definition file)  
     - `host.json` (Logic App runtime config)  
     - `local.settings.json` (local dev settings)  
     - supporting folders (`Artifacts`, `.vscode`, etc.)

2. **Connect Custom .NET Assembly**  
   - Copy the `CustomerValidation.dll` (built from Step 2) into a new `bin` folder inside the Logic App project.  
   - This allows the Logic App workflow to call the DLL methods using the **Execute .NET Function action**.

3. **Configure Workflow**  
   - Open `workflow.json` and design the workflow with the following components:
     - **Trigger**: HTTP Request trigger (to accept incoming customer JSON).  
     - **Action 1**: Parse JSON to extract `email` and `phoneNumber`.  
     - **Action 2**: Execute .NET Function action to call methods inside `CustomerValidation.dll`.  
     - **Action 3**: Add conditional logic to check validation results.  
     - **Action 4**: Response action to return JSON back to the caller.  
   - *(Detailed workflow configuration is already present in the repo under `ValidateCustomerData/workflow.json`)*

4. **Run the Logic App Locally**  
   - Open terminal at the project root.  
   - Start the Functions host with:  
     ```
     func start
     ```  
   - This launches the local Logic App runtime.  
   - The console output will display the **HTTP endpoint URL** (usually `http://localhost:7071/api/…`).

5. **Test with Postman**  
   - Open **Postman** (or cURL).  
   - Create a new **POST request** to the local Logic App endpoint URL.  
   - In the **Body**, select `raw` → `JSON` and provide input in the required format:  
     - Example: `customerId`, `email`, `phoneNumber`.  
   - Send the request.  
   - Verify the response shows validation results:  
     - If both email and phone are valid → returns a **Success response** with confirmation messages.  
     - If invalid → returns an **Error response** with failure details.


## Step 4 – Submission

The GitHub repository includes:

- **CustomerValidation** → DLL project with validation logic  
- **CustomerValidation.Test** → Unit tests for validation logic  
- **LogicAppProj** → Logic App Standard project with workflow  
- **Screenshots** → Folder containing execution proof (tests, local run, Postman results)  
- **README.md** → Documentation file (this file)  

All screenshots of outputs and responses are included in the `Screenshots` folder.

---

## Assumptions

- Validation covers only email format and US phone number format.  
- The project is designed to run locally using Functions Core Tools and Azurite.  
- External deployment to Azure Portal is not required for this evaluation.  
- Screenshots provided serve as proof of correct implementation and execution.  

---
