const mongoose = require('mongoose');

const positionSchema = new mongoose.Schema({
    idUser: { type: mongoose.Schema.Types.ObjectId, ref: 'User', required: true },
    x_position: { type: Number, required: true },
    y_position: { type: Number, required: true },
    z_position: { type: Number, required: true },
    timestamp: { type: Date, default: Date.now }
});

module.exports = mongoose.model('Position', positionSchema);