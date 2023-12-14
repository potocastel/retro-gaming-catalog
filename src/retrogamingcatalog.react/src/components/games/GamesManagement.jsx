import React, { useEffect, useState } from "react";
import EditGame from "./EditGame.jsx";
import GamesList from "./GamesList.jsx";

function GamesManagement() {
  const [selectedId, setSelectedId] = useState(null);
  const [gameSearch, setGameSearch] = useState('');

  const getFeedback = () => {
    setSelectedId(null);
  }
 
  useEffect(() => { 
  }, [gameSearch,selectedId]);

  const handleSearch = (e) => {
    if (e.target.value.length >= 2)
      setGameSearch(e.target.value);
    else if (gameSearch !== '')
      setGameSearch('');
  };

  return (
    <div>
      <div className="d-flex flex-row gap-3">
        <h1 id="tableGames">Games list</h1>
        <button type="button" onClick={() => setSelectedId('00000000-0000-0000-0000-000000000000')} className="btn btn-success align-self-center">
          <i className="bi bi-plus-circle"></i> Add
        </button>
        <input type="text" className=" ml-auto align-self-center pull-right" onChange={handleSearch} />
      </div>
      {selectedId === null ? (
      <GamesList idChanged={setSelectedId} search={gameSearch}/>      
    ) : (
      <EditGame id={selectedId} dataFeedback={getFeedback} />
    )}
    </div>
  );
}

export default GamesManagement;
