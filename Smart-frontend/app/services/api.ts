import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5213/api', // بورت الباك إند الخاص بكِ
  headers: {
    'Content-Type': 'application/json',
  },
});

export default api;
