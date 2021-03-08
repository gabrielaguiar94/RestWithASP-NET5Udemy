import axios from 'axios';

const api = axios.create({
    baseURL:'https://rest-with-asp-net-udemy.herokuapp.com',
})

export default api;