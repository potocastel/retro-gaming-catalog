import React, { useEffect, useState } from "react";

function EditGame({ id, dataFeedback }) {
  const [game, setGame] = useState({
    id: "00000000-0000-0000-0000-000000000000",
    name: "",
    description: "",  
    consoleId: null, 
  });
  const [consoles, setConsoles] = useState();
  const [errorMessage,setErrorMessage]=useState('');

  useEffect(() => {
    loadManufacturers();
    if (id === "00000000-0000-0000-0000-000000000000") {
      setGame({
        id: "00000000-0000-0000-0000-000000000000",
        name: "",
        description: "",  
        consoleId: null, 
      });
    } else {
      getGameData();
    }
    
  }, [id,errorMessage]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    console.log(`handleChange - name: ${name}, value: ${value}`);
  
    setGame((prevGame) => ({
      ...prevGame,
      [name]: value,
    }));
  };
  const handleSubmit = async (e) => {

    e.preventDefault();

    try {
      // Envoyer les données du formulaire à votre API
      const post = id === "00000000-0000-0000-0000-000000000000";
      const response = await fetch("gameslist"+(post?"":"/"+id), {
        method:(post? "POST":"PUT"), // ou "PUT" pour une mise à jour
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(game),
      });
  
      if (response.ok) {
        // Gérer la réussite de la requête, peut-être rediriger ou effectuer d'autres actions
        console.log("Formulaire soumis avec succès !");
      } else {
        console.error("Erreur lors de la soumission du formulaire.");
      }
    } catch (error) {
      console.error("Une erreur s'est produite :", error);
    }    
    dataFeedback();
  };

  const contents =
    game === undefined || consoles === undefined? (
      <p>
        <em>Loading... </em>
      </p>
    ) : (
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label htmlFor="txtGameName" className="form-label">
            Name
          </label>
          <input
            type="text"
            className="form-control"
            id="txtGameName"
            value={game.name}
            name="name"
            onChange={handleChange}
            placeholder="Enter game name"
          />
        </div>
        <div className="mb-3">
          <label htmlFor="txtGameDescription" className="form-label">
            Description
          </label>
          <textarea
            type="text"
            className="form-control"
            id="txtGameDescription"
            value={game.description}
            name="description"
            onChange={handleChange}
            placeholder="Enter game description"
            rows="3"
          ></textarea>
        </div>
        <div className="mb-3">
          <label htmlFor="cmbConsole" className="form-label">
            Console
          </label>
          <select id="cmbConsole" className="form-select" onChange={handleChange} name="consoleId" value={game.consoleId || ""}>
            <option>Select a console</option>
            {
                consoles.map((console)=>(
                    <option key={console.id} value={console.id}>{console.name}</option>

                ))
            }
          </select>
        </div>

        <button type="submit" className="btn btn-primary">
          <i className="bi bi-floppy"></i> Save
        </button>
        <button className="btn btn-danger mx-2" onClick={dataFeedback}>
          <i className="bi bi-x-circle"></i> Cancel
        </button>
      </form>
    );

  return (
    <div>
      <h2>
        {id === "00000000-0000-0000-0000-000000000000"
          ? "New game"
          : "Edit game"}
      </h2>
      {contents}
    </div>
  );

  async function getGameData() {
    if (id === null) return;
    const response = await fetch("gameslist/" + id);
    const data = await response.json();
    setGame(data);
  }

  async function loadManufacturers() {
    const response = await fetch("consolelist");
    const data = await response.json();
    setConsoles(data);
  }
}

export default EditGame;
