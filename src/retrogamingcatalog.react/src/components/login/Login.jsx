import React, { useState } from 'react'; 

const Login = ({onLogin}) => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  const handleLogin = async () => {
    try {

        if(false)
        {
            
        const response = await fetch('https://your-api-endpoint/login', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({ username, password }),
        });
  
        if (!response.ok) {
          throw new Error('Login failed');
        }
  
        const data = await response.json();
  
        // Stockez le token JWT dans le stockage local
        localStorage.setItem('token', data.Token);
    }
    else
    {
    localStorage.setItem('token', 'Fake');
    onLogin();
    }
        // Redirigez l'utilisateur vers une autre page ou effectuez d'autres actions nécessaires
      } catch (error) {
        console.error('Login failed', error);
      }
  };

  return (
    <div className="container mt-5">
      <div className="row justify-content-center">
        <div className="col-md-6">
          <div className="card">
            <div className="card-body">
              <h3 className="card-title text-center mb-4">Login</h3>
              <form>
                <div className="mb-3">
                  <label htmlFor="username" className="form-label">Username</label>
                  <input
                    type="text"
                    className="form-control"
                    id="username"
                    placeholder="Enter your username"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                  />
                </div>
                <div className="mb-3">
                  <label htmlFor="password" className="form-label">Password</label>
                  <input
                    type="password"
                    className="form-control"
                    id="password"
                    placeholder="Enter your password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                  />
                </div>
                <div className="text-center">
                  <button type="button" className="btn btn-primary" onClick={handleLogin}>Login</button>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Login;
