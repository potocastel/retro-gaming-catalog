import React, { useEffect, useState } from "react";

function EditConsole({ id, dataFeedback }) {
  const [console, setConsole] = useState({
    id: "00000000-0000-0000-0000-000000000000",
    name: ""
  });

  const [manufacturers, setManufacturers] = useState([]);


  useEffect(() => {
    loadManufacturers();
    if (id!== null && id !== "00000000-0000-0000-0000-000000000000") {
      getConsoleData();
    }    
  }, [id]);

  const handleChange = (e) => {
    const { name, value } = e.target;
  
    setConsole((prevConsole) => ({
      ...prevConsole,
      [name]: value,
    }));
  };
  const handleSubmit = async (e) => {

    e.preventDefault();

    try { 
      const post = id === "00000000-0000-0000-0000-000000000000";
      const response = await fetch("consolelist"+(post?"":"/"+id), {
        method:(post? "POST":"PUT"), // ou "PUT" pour une mise à jour
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(console),
      });
  
      if (response.ok) {
        dataFeedback();
      } else {
        console.error("Erreur lors de la soumission du formulaire.");
      }
    } catch (error) {
      console.error("Une erreur s'est produite :", error);
    }    
    
  };

  const contents =
    console === undefined ? (
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
            value={console.name}
            name="name"
            onChange={handleChange}
            placeholder="Enter console name"
          />
        </div>
        <div className="mb-3">
          <label htmlFor="cmbManufacturer" className="form-label">
            Manufacturer
          </label>
          <select id="cmbManufacturer" className="form-select" onChange={handleChange} name="manufacturerId" value={console.manufacturerId || ""}>
            <option>Select a manufacturer</option>
            {
                manufacturers.map((manufacturer)=>(
                    <option key={manufacturer.id} value={manufacturer.id}>{manufacturer.name}</option>

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
          ? "New console"
          : "Edit console"}
      </h2>
      {contents}
    </div>
  );

  async function getConsoleData() {
    
    const response = await fetch("consolelist/" + id);
    const data = await response.json();
    setConsole(data);
  }

  async function loadManufacturers() {
    const response = await fetch("manufacturerlist");
    const data = await response.json();
    setManufacturers(data);
  }
}

export default EditConsole;
