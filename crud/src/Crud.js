import React, { useEffect, useState } from "react";
import Table from "react-bootstrap/Table";
import "bootstrap/dist/css/bootstrap.min.css";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";
import Container from "react-bootstrap/Container";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import axios from "axios";
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const Crud = () => {
  const [data, setData] = useState([]);
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const [name, setName] = useState("");
  const [age, setAge] = useState("");
  const [isactive, setIsactive] = useState(0);

  const [editId, setEditId] = useState("");
  const [editname, setEditName] = useState("");
  const [editage, setEditAge] = useState("");
  const [editisActive, setEditIsactive] = useState(0);

  const handleEdit = (id) => {
    handleShow();
    axios.get(`https://localhost:7271/api/Employees/${id}`)
    .then((result)=>{
      setEditName(result.data.name);
      setEditAge(result.data.age);
     setEditIsactive(result.data.isactive === 1 || result.data.isactive === "1");
   
      setEditId(id);
     
    })
    .catch((error) => {
      toast.error(error);
    });
  };

  const handleDelete = (id) => {
    if (window.confirm("Are You sure delete") == true) {
     axios.delete(`https://localhost:7271/api/Employees/${id}`)
     .then((result)=>{
      if(result.status===200){
        toast.success("Employee Has Been Deleted");
        getData();
      }
     }).catch((error) => {
      toast.error(error);
    });
    }
  };

  const handleUpdate = () => {

    const url=`https://localhost:7271/api/Employees/${editId}`;
    const data={
      "Id":editId,
      "Name": editname,
      "Age": editage,
      "Isactive": editisActive.toString() // Convert to string if needed meny database mai string kara hy 
    };
    axios.put(url, data).then((result) => {
      handleClose();
      getData();
      clear();
      toast.success("Employee Has Been Updated");
    }).catch((error) => {
      toast.error(error);
    });

    
  };

  const getData = () => {
    axios
      .get("https://localhost:7271/api/Employees")
      .then((result) => {
        setData(result.data)
      })
      .catch((error) => {
        console.log(error);
      });
  };

  useEffect(() => {
    getData()
  }, []);

  const handleSave = () => {
    const url = 'https://localhost:7271/api/Employees';
    const data = {
      "Name": name,
      "Age": age,
      "Isactive": isactive.toString() // Convert to string if needed meny database mai string kara hy 
    };
    axios.post(url, data).then((result) => {
      getData();
      clear();
      toast.success("Employee Has Been Added");
    }).catch((error) => {
      toast.error(error);
    });
  };

  const clear = () => {
    setName('');
    setAge('');
    setIsactive(0);
    setEditName('');
    setEditAge('');
    setEditIsactive(0);
    setEditId('');
  };

  const handleActiveChange = (e) => {
    if(e.target.checked){
      setIsactive(1);
         }
         else{
          setIsactive(0);
         }
  };

  const handleEditActiveChange = (e) => {
    if(e.target.checked){
      setEditIsactive(1);
         }
         else{
          setEditIsactive(0);
         }
  };

  return (
    <>
      <ToastContainer />
      <Container>
        <Row className="d-flex align-items-center pt-5 pb-5">
          <Col>
            <input
              type="text"
              className="form-control"
              placeholder="Enter Name"
              value={name}
              onChange={(e) => setName(e.target.value)}
            />
          </Col>
          <Col>
            <input
              type="text"
              className="form-control"
              placeholder="Enter Age"
              value={age}
              onChange={(e) => setAge(e.target.value)}
            />
          </Col>
          <Col>
            <input
              type="checkbox"
              value={isactive}
              checked={isactive===1?true:false}
onChange={(e)=>handleActiveChange(e)}
            />
            <label>IsActive</label>
          </Col>
          <Col>
            <Button variant="primary" onClick={handleSave}>Submit</Button>
          </Col>
        </Row>
      </Container>

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Modify / Update Employee</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Row className="d-flex align-items-center">
            <Col>
              <input
                type="text"
                className="form-control"
                placeholder="Enter Name"
                value={editname}
                onChange={(e) => setEditName(e.target.value)}
              />
            </Col>
            <Col>
              <input
                type="text"
                className="form-control"
                placeholder="Enter Age"
                value={editage}
                onChange={(e) => setEditAge(e.target.value)}
              />
            </Col>
            <Col>
              <input
                       type="checkbox"
                       checked={editisActive ===1?true:false}
                       onChange={(e)=>handleEditActiveChange(e)} value={editisActive}
              />
              <label>IsActive</label>
            </Col>
          </Row>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
          <Button variant="primary" onClick={handleUpdate}>
            Save Changes
          </Button>
        </Modal.Footer>
      </Modal>

      <Table striped bordered hover className="text-center">
        <thead>
          <tr>
            <th>#</th>
            <th>Name</th>
            <th>Age</th>
            <th>IsActive</th>
          </tr>
        </thead>
        <tbody>
          {data && data.length > 0
            ? data.map((item, index) => (
              <tr key={index}>
                <td>{index + 1}</td>
                <td>{item.name}</td>
                <td>{item.age}</td>
                <td>{item.isactive}</td>
                <td>
                  <button
                    type="button"
                    className="btn btn-primary"
                    onClick={() => handleEdit(item.id)}
                  >
                    Edit
                  </button>
                  &nbsp;
                  <button
                    type="button"
                    className="btn btn-danger"
                    onClick={() => handleDelete(item.id)}
                  >
                    Delete
                  </button>
                </td>
              </tr>
            ))
            : "Swagger API closed or loading..."}
        </tbody>
      </Table>
    </>
  );
};

export default Crud;
