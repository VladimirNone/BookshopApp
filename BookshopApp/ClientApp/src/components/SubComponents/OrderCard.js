import React, { Component } from 'react';

export class OrderCard extends Component {

    render() {
        const order = this.props.order;

        return (
            <div className="card mt-3 bg-light">
                <div className="row no-gutters align-items-center ordered_card">
                    <div className="col-md">
                        <div className="card-body">
                            <h5 className="card-title">Номер: {order.id}</h5>
                            <p className="card-text">Дата оформления заказа: {new Date(order.dateOfOrdering).toLocaleString()}</p>
                            <p className="card-text">Дата закрытия заказа: {new Date(order.dateOfClosing).toLocaleString()}</p>
                            <p className="card-text">Стоимость:{order.finalAmount}</p>
                            <p className="card-text">Статус: {order.state.nameOfState}</p>
                        </div>
                    </div>
                </div>
            </div>);
    }
}