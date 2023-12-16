import React, { useEffect, useState } from "react";
import ConsoleList from "./ConsoleList.jsx";
import EditConsole from "./EditConsole.jsx";

function ConsoleManagement() {
  const [selectedId, setSelectedId] = useState(null); 

  useEffect(() => { 
  }, [selectedId]);

  const closeEdit = () => {
    setSelectedId(null);
  }

  return (
    <div>
      <div className="d-flex flex-row gap-3">
        <h1 id="tableConsole">Console list</h1> 
        <button type="button" onClick={() => setSelectedId('00000000-0000-0000-0000-000000000000')} className="btn btn-success align-self-center">
          <i className="bi bi-plus-circle"></i> Add
        </button>
      </div>
      {
        selectedId===null ? (
        <ConsoleList idChanged={setSelectedId}/> ) : (
          <EditConsole id={selectedId} dataFeedback={closeEdit} />
        )
      }
    </div>
  ); 
}

export default ConsoleManagement;
