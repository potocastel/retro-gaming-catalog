import React, { useEffect, useState } from "react";

function EditManufacturer({ id, dataFeedback }) {
  const [manufacturer, setManufacturer] = useState({
    id: "00000000-0000-0000-0000-000000000000",
    name: ""
  });

  useEffect(() => {
    if (id !== "00000000-0000-0000-0000-000000000000") {
      getManufacturerData();
    }    
  }, [id]);

  const handleChange = (e) => {
    const { name, value } = e.target;
  
    setManufacturer((prevManufacturer) => ({
      ...prevManufacturer,
      [name]: value,
    }));
  };
  const handleSubmit = async (e) => {

    e.preventDefault();

    try { 
      const post = id === "00000000-0000-0000-0000-000000000000";
      const response = await fetch("manufacturerlist"+(post?"":"/"+id), {
        method:(post? "POST":"PUT"), // ou "PUT" pour une mise à jour
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(manufacturer),
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
    manufacturer === undefined ? (
      <p>
        <em>Loading... </em>
      </p>
    ) : (
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label htmlFor="txtName" className="form-label">
            Name
          </label>
          <input
            type="text"
            className="form-control"
            id="txtName"
            value={manufacturer.name}
            name="name"
            onChange={handleChange}
            placeholder="Enter manufacturer name"
          />
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
          ? "New manufacturer"
          : "Edit manufacturer"}
      </h2>
      {contents}
    </div>
  );

  async function getManufacturerData() {
    if (id === null) return;
    const response = await fetch("manufacturerlist/" + id);
    const data = await response.json();
    setManufacturer(data);
  }
}

export default EditManufacturer;
