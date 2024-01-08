import React, { useEffect, useState } from "react";
import api from '../api/api.js';

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
      const url ="manufacturerlist"+(post?"":"/"+id);
      const response = await (post?api.post(url,manufacturer):api.put(url,manufacturer));  
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
    const response = await api.get("manufacturerlist/" + id);
    const data = await response.json();
    setManufacturer(data);
  }
}

export default EditManufacturer;
