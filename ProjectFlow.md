# Todo App Project Flow

This document outlines the development process and architecture of the Todo App, a full-stack application with a React frontend and a C# backend.

## 1. Project Setup

### Backend (C# with ASP.NET Core)

1.  **Project Initialization**: A new ASP.NET Core Web API project was created.
2.  **Folder Structure**: The backend was organized into the following folders to ensure separation of concerns:
    *   `Models`: Contains the data models (`TodoItem.cs`) and Data Transfer Objects (DTOs) (`TodoDtos.cs`).
    *   `Data`: Includes the `TodoDbContext.cs` for database interactions using Entity Framework Core. An in-memory database is used for simplicity.
    *   `Interfaces`: Defines the contract for the todo service (`ITodoService.cs`).
    *   `Services`: Implements the business logic for todo operations in `TodoService.cs`.
    *   `Controllers`: Exposes the API endpoints for CRUD operations in `TodosController.cs`.
3.  **Dependencies**: The following NuGet packages were added:
    *   `Microsoft.EntityFrameworkCore.InMemory`: For the in-memory database.
    *   `Microsoft.AspNetCore.Cors`: To handle Cross-Origin Resource Sharing (CORS) with the React frontend.
    *   `Swashbuckle.AspNetCore`: For API documentation using Swagger.
4.  **Configuration**:
    *   `Program.cs` was configured to set up dependency injection for the `ITodoService`, configure the database context, and enable CORS.
    *   The CORS policy was specifically configured to allow requests from the React development server.

### Frontend (React with TypeScript)

1.  **Project Initialization**: A new React application was created using `create-react-app` with the TypeScript template.
2.  **Folder Structure**: The frontend was organized as follows:
    *   `src/components`: Contains reusable React components like `TodoForm.tsx`, `TodoList.tsx`, and `TodoItem.tsx`.
    *   `src/hooks`: Includes the `useTodos.ts` custom hook to manage the state and logic for fetching and manipulating todos.
    *   `src/services`: Contains the `todoApi.ts` service, which uses `axios` to communicate with the C# backend.
    *   `src/types`: Defines the TypeScript types for the todo items (`todo.ts`).
3.  **Dependencies**:
    *   `axios`: For making HTTP requests to the backend API.
4.  **Styling**:
    *   A simple and clean CSS file (`App.css`) was created to style the application.

## 2. Backend Development

1.  **Model and DTOs**: The `TodoItem` model was created with properties like `Id`, `Title`, `Description`, `IsCompleted`, etc. DTOs (`CreateTodoDto`, `UpdateTodoDto`) were created to handle data transfer for creating and updating todos.
2.  **Database Context**: The `TodoDbContext` was set up to use an in-memory database and seeded with initial data for testing.
3.  **Service Layer**: The `TodoService` was implemented with methods for all CRUD operations, following the `ITodoService` interface. This service contains all the business logic.
4.  **Controller**: The `TodosController` was created with API endpoints for each CRUD operation. It uses the `ITodoService` through dependency injection to perform the operations.
5.  **CORS**: CORS was enabled in `Program.cs` to allow the frontend application (running on a different port) to make requests to the backend.

## 3. Frontend Development

1.  **API Service**: The `todoApi.ts` service was created to encapsulate all API calls to the backend. It uses an `axios` instance with a base URL pointing to the backend.
2.  **Custom Hook**: The `useTodos.ts` custom hook was developed to manage the state of the todos, including loading and error states. It provides functions to fetch, create, update, and delete todos.
3.  **Components**:
    *   `TodoForm.tsx`: A form for adding new todos.
    *   `TodoItem.tsx`: A component to display a single todo item, with buttons for editing and deleting.
    *   `TodoList.tsx`: A component to display the list of all todos.
    *   `App.tsx`: The main application component that brings all the other components together.
4.  **Styling**: The `App.css` file was updated with simple and modern styles to make the application visually appealing.

## 4. Connecting Frontend and Backend

The frontend and backend are connected via HTTP requests. The React application's `todoApi` service makes calls to the C# backend's API endpoints. The backend's CORS policy ensures that these requests are not blocked by the browser.

This structured approach ensures a clean, maintainable, and scalable application.
