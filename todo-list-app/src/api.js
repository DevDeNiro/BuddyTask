import axios from "axios";
const apiClient = axios.create({
  baseURL: "/api",
  headers: {
    Accept: "application/json",
    ContentType: "application/json",
  },
});

export default apiClient;
