import React, { useEffect, useState } from "react";
import EditGame from "./EditGame.jsx";

function GamesList() {
  const [games, setGames] = useState();
  const [selectedId, setSelectedId] = useState(null);
  const [gameSearch, setGameSearch] = useState('');

  const getFeedback = () => {
    setSelectedId(null);
  }

  useEffect(() => {
    populateGamesList();
  }, [gameSearch]);

  const handleSearch = (e) => {
    console.log(e.target.value);
    console.log(e.target.value!=='');
    if (e.target.value.length >= 2)
      setGameSearch(e.target.value);
    else if (gameSearch !== '')
      setGameSearch('');
  };

  const contents =
    selectedId === null ? (
      games === undefined ? (
        <p>
          <em>Loading... </em>
        </p>
      ) : (
        <table className="table table-striped" aria-labelledby="tableGames">
          <thead>
            <tr>
              <th>Name</th>
              <th>Console</th>
              <th>Manufacturer</th>
              <th>#</th>
            </tr>
          </thead>
          <tbody>
            {games.map((game) => (
              <tr key={game.id}>
                <td>{game.name}</td>
                <td>{game.consoleName}</td>
                <td>{game.manufacturerName}</td>
                <td>
                  <button
                    type="button"
                    className="btn btn-sm btn-primary mx-1"
                    onClick={() => setSelectedId(game.id)}
                  >
                    <i className="bi bi-pencil-square"></i> Edit
                  </button>
                  <button type="button" className="btn btn-sm btn-danger mx-1">
                    <i className="bi bi-x-circle"></i> Delete
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )
    ) : (
      <EditGame id={selectedId} dataFeedback={getFeedback} />
    );

  return (
    <div>
      <div className="d-flex flex-row gap-3">
        <h1 id="tableGames">Games list</h1>
        <button type="button" onClick={() => setSelectedId('00000000-0000-0000-0000-000000000000')} className="btn btn-success align-self-center">
          <i className="bi bi-plus-circle"></i> Add
        </button>
        <input type="text" className=" ml-auto align-self-center pull-right" onChange={handleSearch} />
      </div>
      {contents}
    </div>
  );
  async function populateGamesList() {
    var url = 'gameslist';
    if (gameSearch !== '')
      url += '/byname/' + gameSearch;

    const response = await fetch(url);
    const data = await response.json();
    setGames(data);
  }
}

export default GamesList;
