import React, { useState, useEffect } from "react";
import { Link, useHistory } from "react-router-dom";
import { FiPower, FiEdit, FiTrash2 } from "react-icons/fi";
import api from "../../services/api";
import "./styles.css";
import logoImage from "../../assets/logo.svg";

export default function Books() {
  
  const [books, setBooks] = useState([]);
  const [page, setPage] = useState(1);

  const userName = localStorage.getItem("userName");
  const accessToken = localStorage.getItem("accessToken");

  const authorization = {
    headers: {
      Authorization: `Bearer ${accessToken}`,
    },
  }; 

  const history = useHistory();

  useEffect(() => {
      fetchMoreBooks();
  },[accessToken,fetchMoreBooks]);  

  // eslint-disable-next-line react-hooks/exhaustive-deps
  async function fetchMoreBooks()  {
    const response = await api.get(`api/books/v1/asc/4/${page}`, authorization);
        setBooks([...books, ...response.data.list]);
        setPage(page + 1);      
  }

  async function editBook(id){
      try {

        history.push(`book/new/${id}`);
        
          
      } catch (error) {
          alert('Edit Book failed!Try again!');          
      }
  }

  async function deleteBook(id){
    try {
      await api.delete(`api/books/v1/${id}`, authorization);

        setBooks(books.filter(book => book.id !== id));
        
    } catch (error) {
        alert('Delete failed!Try again!');          
    }
}

  async function logout(){
    try {
      await api.get(`api/Auth/v1/revoke`, authorization); 
        localStorage.clear();
        history.push('/');
        
    } catch (error) {
        alert('Logout failed!Try again!');          
    }
}

  return (
    <div className="book-container">
      <header>
        <img src={logoImage} alt="Erudio" />
        <span>
          Welcome, <strong>{userName.toUpperCase()}</strong>
        </span>
        <Link className="button" to="book/new/0">
          Add New Book
        </Link>
        <button type="button">
          <FiPower onClick={logout}size={18} color="#251FC5" />
        </button>
      </header>

      <h1>Registered Books</h1>
      <ul>
        {books.map((book) => (
          <li key={book.id}>
            <strong>Title:</strong>
            <p>{book.title}</p>
            <strong>Author:</strong>
            <p>{book.author}</p>
            <strong>Price:</strong>
            <p>{Intl.NumberFormat('pt-BR',{style: 'currency', currency: 'BRl'}).format(book.price)}</p>
            <strong>Release Date:</strong>
            <p>{Intl.DateTimeFormat('pt-BR').format(new Date(book.launchedDate))}</p>
            <button type="button">
              <FiEdit onClick={() => editBook(book.id)} size={20} color="251FC5" />
            </button>
            <button 
                type="button"
                onClick={() => deleteBook(book.id)}
            >
              <FiTrash2 size={20} color="251FC5" />
            </button>
          </li>
        ))}
      </ul>
      <button className="button" onClick={fetchMoreBooks} type="button">Load More</button>
    </div>
  );
}
