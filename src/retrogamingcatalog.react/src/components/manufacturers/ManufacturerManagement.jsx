import React, { useEffect, useState } from "react";
import ManufacturerList from "./ManufacturerList.jsx";
import EditManufacturer from "./EditManufacturer.jsx";

function ManufacturerManagement() {
  const [selectedId, setSelectedId] = useState(null); 

  useEffect(() => { 
  }, [selectedId]);

  const closeEdit = () => {
    setSelectedId(null);
  }

  return (
    <div>
      <div className="d-flex flex-row gap-3">
        <h1 id="tableManufacturers">Manufacturer list</h1> 
        <button type="button" onClick={() => setSelectedId('00000000-0000-0000-0000-000000000000')} className="btn btn-success align-self-center">
          <i className="bi bi-plus-circle"></i> Add
        </button>
      </div>
      {
        selectedId===null ? (
        <ManufacturerList idChanged={setSelectedId}/> ) : (
          <EditManufacturer id={selectedId} dataFeedback={closeEdit} />
        )
      }
    </div>
  ); 
}

export default ManufacturerManagement;
