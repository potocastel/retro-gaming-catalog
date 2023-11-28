import React, { useEffect, useState } from 'react';

function GamesList() {

    const [games, setGames] = useState();

    useEffect(() => {
        populateGamesList();
    }, []);


    const contents = games === undefined
        ? <p><em>Loading... </em></p>
: <table className="table table-striped" aria-labelledby="tableGames">
      <thead>
      <tr>
          <th>Name</th>
          <th>Console</th>
          <th>Manufacturer</th>
      </tr>
      </thead>
      <tbody>
      {games.map(game =>
          <tr key={game.id}>
              <td>{game.name}</td>
              <td>{game.consoleName}</td>
              <td>{game.manufacturerName}</td>
          </tr>
      )}
      </tbody>
  </table>;

return (
<div>
    <h1 id="tableGames">Games list</h1>
    {contents}
</div>
); 
  async function populateGamesList() {
      const response = await fetch('gameslist');
      const data = await response.json();
      setGames(data);
  }
}

export default GamesList;