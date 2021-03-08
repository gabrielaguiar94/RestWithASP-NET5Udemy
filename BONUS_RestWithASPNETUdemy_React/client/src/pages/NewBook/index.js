import React, { useEffect, useState } from "react";
import { Link, useHistory, useParams } from "react-router-dom";
import { FiArrowLeft } from "react-icons/fi";
import logoImage from "../../assets/logo.svg";
import api from "../../services/api";
import "./styles.css";

export default function NewBook() {
  const [id, setId] = useState(null);
  const [author, setAuthor] = useState("");
  const [title, setTitle] = useState("");
  const [launchedDate, setLaunchedDate] = useState("");
  const [price, setPrice] = useState("");

  const { bookId } = useParams();

  const history = useHistory();

  const accessToken = localStorage.getItem("accessToken");

  const authorization = {
    headers: {
      Authorization: `Bearer ${accessToken}`,
    },
  };

  useEffect(() => {
    if (bookId === "0") return;
    else loadBook();
  });

  async function loadBook() {
    try {
      const response = await api.get(`api/books/v1/${bookId}`, authorization);

      let adjustedDate = response.data.launchedDate.split("T", 10)[0];

      setId(response.data.id);
      setTitle(response.data.title);
      setAuthor(response.data.author);
      setLaunchedDate(adjustedDate);
      setPrice(response.data.price);
    } catch (error) {
      alert("Erro recovering book!");
      history.push("/books");
    }
  }

  async function saveOrUpdate(e) {
    e.preventDefault();

    const data = {
      title,
      author,
      launchedDate,
      price,
    };

    try {
      if (bookId === "0") {
        await api.post("api/books/v1", data, authorization);
      } else {
        data.id = id;
        await api.put("api/books/v1", data, authorization);
      }
    } catch (error) {
      alert("Error while recording book! Try again!");
    }
    history.push("/books");
  }

  return (
    <div className="new-book-container">
      <div className="content">
        <section className="form">
          <img src={logoImage} alt="Erudio" />
          <h1>{bookId === "0" ? "Add New Book" : "Update a book"}</h1>
          <p>
            Enter the Book information and click
            {bookId === "0" ? " on Add" : " Update book"}
          </p>
          <Link className="back-link" to="/books">
            <FiArrowLeft size={16} color="#251FC5" />
            Back to Books
          </Link>
        </section>
        <form onSubmit={saveOrUpdate}>
          <input
            placeholder="Title"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
          />
          <input
            placeholder="Author"
            value={author}
            onChange={(e) => setAuthor(e.target.value)}
          />
          <input
            type="date"
            value={launchedDate}
            onChange={(e) => setLaunchedDate(e.target.value)}
          />
          <input
            placeholder="Price"
            value={price}
            onChange={(e) => setPrice(e.target.value)}
          />
          <button className="button" type="submit">
            {bookId === "0" ? "Add" : "Update Book"}
          </button>
        </form>
      </div>
    </div>
  );
}
