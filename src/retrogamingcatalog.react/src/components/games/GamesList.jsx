import React, { useEffect, useState } from "react";
import EditGame from "./EditGame.jsx";

function GamesList(props) {
  const [games, setGames] = useState();
  const [selectedId, setSelectedId] = useState(null);

  const getFeedback = () => {
    setSelectedId(null);
  }

  useEffect(() => {
    populateGamesList(props.search); 
  }, [props.search]);

  const idSelected = (id) => {
    setSelectedId(id);
    props.idChanged(id);
  };
 
  return (
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
                  onClick={()=>idSelected(game.id)}
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
      </table>)
  );
  async function populateGamesList(gameSearch) {
    var url = 'gameslist';
    if (gameSearch != '')
      url += '/byname/' + gameSearch;

    const response = await fetch(url);
    const data = await response.json();
    setGames(data);
  }
}

export default GamesList;
