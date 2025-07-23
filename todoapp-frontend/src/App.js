import React, { useState, useEffect } from 'react'; // Import React hooks
import axios from 'axios'; // Import Axios for HTTP requests
import './App.css'; // Import the CSS file for styling

// Define the base URL for your .NET API
// IMPORTANT: Make sure this matches the URL your .NET API is running on
// (e.g., from the console output when you run 'dotnet run' for TodoApp.Api)
const API_BASE_URL = 'http://localhost:5093/api/TodoItems';

function App() {
    // --- State Variables ---
    // `todos`: Array to store the list of to-do items fetched from the API
    // `setTodos`: Function to update the `todos` state
    const [todos, setTodos] = useState([]);
    // `newTodoTitle`: Stores the text entered in the "Add new to-do" input field
    const [newTodoTitle, setNewTodoTitle] = useState('');
    // `editingTodo`: Stores the to-do item object that is currently being edited.
    // Null if no item is being edited.
    const [editingTodo, setEditingTodo] = useState(null);

    // --- useEffect Hook ---
    // Runs once after the component mounts (like componentDidMount)
    useEffect(() => {
        fetchTodos(); // Fetch todos when the component first loads
    }, []); // Empty dependency array means it runs only once

    // --- API Interaction Functions ---

    // Fetches all to-do items from the API
    const fetchTodos = async () => {
        try {
            const response = await axios.get(API_BASE_URL); // Make a GET request
            setTodos(response.data); // Update the state with the fetched data
        } catch (error) {
            console.error('Error fetching todos:', error);
            alert('Error fetching todos. Make sure the backend API is running!');
        }
    };

    // Adds a new to-do item to the API
    const handleAddTodo = async () => {
        if (!newTodoTitle.trim()) { // Basic client-side validation: check if title is empty or just whitespace
            alert('Todo title cannot be empty!');
            return;
        }
        try {
            // Make a POST request with the new todo title
            const response = await axios.post(API_BASE_URL, { title: newTodoTitle });
            // Add the newly created todo (from API response) to the state
            setTodos([...todos, response.data]);
            setNewTodoTitle(''); // Clear the input field
        } catch (error) {
            console.error('Error adding todo:', error);
            // Display a more specific error message from the API if available
            alert('Error adding todo: ' + (error.response?.data || error.message));
        }
    };

    // Deletes a to-do item by its ID
    const handleDeleteTodo = async (id) => {
        try {
            await axios.delete(`${API_BASE_URL}/${id}`); // Make a DELETE request
            // Remove the deleted todo from the state
            setTodos(todos.filter(todo => todo.id !== id));
        } catch (error) {
            console.error('Error deleting todo:', error);
            alert('Error deleting todo: ' + (error.response?.data || error.message));
        }
    };

    // Toggles the 'IsCompleted' status of a to-do item
    const handleToggleComplete = async (todo) => {
        const updatedTodo = { ...todo, isCompleted: !todo.isCompleted }; // Create a new object with toggled status
        try {
            // Make a PUT request to update the todo item
            await axios.put(`${API_BASE_URL}/${todo.id}`, updatedTodo);
            // Update the state with the modified todo item
            setTodos(todos.map(t => (t.id === todo.id ? updatedTodo : t)));
        } catch (error) {
            console.error('Error updating todo:', error);
            alert('Error updating todo: ' + (error.response?.data || error.message));
        }
    };

    // Enters editing mode for a specific to-do item
    const handleEditClick = (todo) => {
        setEditingTodo({ ...todo }); // Store a copy of the todo item to be edited
    };

    // Saves the changes made during editing an item
    const handleUpdateTodo = async () => {
        if (!editingTodo.title.trim()) { // Validation for updated title
            alert('Todo title cannot be empty!');
            return;
        }
        try {
            // Make a PUT request to update the todo item with edited details
            await axios.put(`${API_BASE_URL}/${editingTodo.id}`, editingTodo);
            // Update the state with the modified todo item
            setTodos(todos.map(t => (t.id === editingTodo.id ? editingTodo : t)));
            setEditingTodo(null); // Exit editing mode
        } catch (error) {
            console.error('Error updating todo:', error);
            alert('Error updating todo: ' + (error.response?.data || error.message));
        }
    };

    // --- JSX (UI Rendering) ---
    return (
        <div className="App">
            <h1>My To-Do List</h1>

            <div className="add-todo">
                <input
                    type="text"
                    placeholder="Add a new to-do"
                    value={newTodoTitle} // Controlled component: input value tied to state
                    onChange={(e) => setNewTodoTitle(e.target.value)} // Update state on input change
                />
                <button onClick={handleAddTodo}>Add Todo</button>
            </div>

            <ul className="todo-list">
                {todos.map((todo) => ( // Map over the 'todos' array to render each item
                    <li key={todo.id} className={todo.isCompleted ? 'completed' : ''}>
                        {editingTodo && editingTodo.id === todo.id ? ( // Conditional rendering for editing mode
                            <>
                                <input
                                    type="text"
                                    value={editingTodo.title}
                                    onChange={(e) => setEditingTodo({ ...editingTodo, title: e.target.value })}
                                />
                                <button onClick={handleUpdateTodo}>Save</button>
                                <button onClick={() => setEditingTodo(null)}>Cancel</button>
                            </>
                        ) : (
                            // Display mode
                            <>
                                <span onClick={() => handleToggleComplete(todo)}>{todo.title}</span>
                                <button onClick={() => handleEditClick(todo)}>Edit</button>
                                <button onClick={() => handleDeleteTodo(todo.id)}>Delete</button>
                            </>
                        )}
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default App; // Export the App component