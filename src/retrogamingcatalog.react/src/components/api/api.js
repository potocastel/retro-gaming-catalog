// api.js

const getToken = () => localStorage.getItem('token');

const apiRequest = async (url, method, body = null) => {
  const token = getToken();
  const headers = {
    'Content-Type': 'application/json',
    Authorization: token ? `Bearer ${token}` : '',
  };

  const options = {
    method,
    headers,
  };

  if (body) {
    options.body = JSON.stringify(body);
  }

  const response = await fetch(url, options);

  if (!response.ok) {
    throw new Error('La requête a échoué');
  }

  return response;
};

const api = {
  get: (url) => apiRequest(url, 'GET'),
  post: (url, body) => apiRequest(url, 'POST', body),
  put: (url, body) => apiRequest(url, 'PUT', body),
  // Ajoutez d'autres méthodes (delete, patch, etc.) au besoin
};

export default api;
